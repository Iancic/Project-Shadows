using System;
using UnityEngine;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;

public class CleanupItem : MonoBehaviour
{
    public int Reward;
    public Texture2D Texture;

    public ComputeShader VisualCS;
    public ComputeShader EvalCS;

    private Mop mop;
    [SerializeField] private Texture2D dirtMask;
    [SerializeField] private RenderTexture dirtMaskCS;
    private Material material;
    private int _initialDirtyPixels = 0; // TODO: Might be used if we want to calculate the percentage of cleaned pixels
    private Vector3 _lastMopPos;
    private int dirtyPixelsLeft = Int32.MaxValue;
    private bool waitingForReadback = false;
    private float dirtinessReadBackInterval = 0.2f;
    private float lastDirtinessReadBackTime = 0;

    private void Start()
    {
        EvalCS = Instantiate(Resources.Load("BloodEval")) as ComputeShader;
        VisualCS = Instantiate(Resources.Load("BloodVisual")) as ComputeShader;

        // Create the dirtmask 
        // TODO: Maybe move this to GPU too?
        dirtMask = new Texture2D(Texture.width, Texture.height);
        for (int x = 0; x < Texture.width; x++)
        {
            for (int y = 0; y < Texture.height; y++)
            {
                // if the pixel is not transparent, set it to green
                if (Texture.GetPixel(x, y).a > 0.1)
                {
                    dirtMask.SetPixel(x, y, Color.green);
                    _initialDirtyPixels++;
                }
                else
                {
                    dirtMask.SetPixel(x, y, Color.clear);
                }
            }
        }

        dirtMask.Apply();

        material = GetComponent<Renderer>().material;

        dirtMaskCS = new RenderTexture(Texture.width, Texture.height, 0);
        dirtMaskCS.enableRandomWrite = true;

        material.SetTexture("_DirtMask", dirtMaskCS);
        material.SetTexture("_DirtyTex", Texture);

        VisualCS.SetTexture(0, "Result", dirtMaskCS);
        VisualCS.SetTexture(1, "Result", dirtMaskCS);
        VisualCS.SetInt("DirtMaskTemplateWidth", dirtMask.width);
        VisualCS.SetInt("DirtMaskTemplateHeight", dirtMask.height);

        VisualCS.SetTexture(1, "DirtMaskTemplate", dirtMask);
        VisualCS.Dispatch(1, Texture.width / 8, Texture.height / 8, 1);
    }

    public void InjectMopRef(Mop m) => mop = m;

    private void Update()
    {
        VisualCS.SetTexture(0, "Result", dirtMaskCS);
        VisualCS.Dispatch(0, Texture.width / 8, Texture.height / 8, 1);
        if (mop == null) return;
        if (Vector3.Distance(mop.GetMopTipPosition(), _lastMopPos) < 0.1f) return;

        _lastMopPos = mop.GetMopTipPosition();

        if (dirtyPixelsLeft < 2500) dirtyPixelsLeft = 0;

        RaycastHit hit;
        if (Physics.Raycast(_lastMopPos, Vector3.down, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= Texture.width;
                pixelUV.y *= Texture.height;

                VisualCS.SetVector("hitUV", new Vector4(pixelUV.x, pixelUV.y, 0, 0));
                EvaluateDirtiness();
            }
        }
    }

    private void LateUpdate()
    {
        if (dirtyPixelsLeft <= 0)
        {
            Debug.Log("Cleaned!");
            Destroy(gameObject);
        }
    }

    public void EvaluateDirtiness()
    {
        if (waitingForReadback) return;
        if (Time.time - lastDirtinessReadBackTime < dirtinessReadBackInterval) return;

        lastDirtinessReadBackTime = Time.time;
        ComputeBuffer resultBuffer = new ComputeBuffer(1, sizeof(int));

        EvalCS.SetBuffer(0, "Result", resultBuffer);
        EvalCS.SetTexture(0, "DirtMask", dirtMaskCS);
        EvalCS.SetInt("DirtMaskSize", dirtMask.width);
        EvalCS.Dispatch(0, 1, 1, 1);

        waitingForReadback = true;
        AsyncGPUReadback.Request(resultBuffer, (request) =>
        {
            if (request.hasError)
            {
                Debug.LogError("GPU readback error detected.");
                waitingForReadback = false;
                return;
            }

            int res = request.GetData<int>()[0];
            dirtyPixelsLeft = res;
            waitingForReadback = false;

            resultBuffer.Release();
        });
    }
}
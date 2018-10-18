using UnityEngine;

public class BezierWalker : MonoBehaviour
{
    public BezierCurve spline;

    public float duration;

    public bool lookForward;

    public SplineWalkerMode mode;

    private float progress;
    private bool goingForward = true;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (goingForward)
        {
            progress += Time.deltaTime / duration;

            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;

            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        if (trailRenderer != null && (progress < 0.10f || progress > 0.90f))
            trailRenderer.Clear();

        trailRenderer.enabled = (progress > 0.10f && progress < 0.90f);

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;

        if (lookForward)
            transform.LookAt(position + spline.GetDirection(progress));
    }
}
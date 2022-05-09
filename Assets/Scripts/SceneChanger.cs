using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public Animator _animator;
    public static bool toTriggerFadeOut = false;
    // Update is called once per frame
    void Update()
    {
        FadeOutToScene();
        FadeInToScene();
    }

    private void FadeOutToScene()
    {
        if (toTriggerFadeOut)
        {
            _animator.SetTrigger("FadeOut");
        }
    }

    public void FadeInToScene()
    {
        _animator.SetTrigger("FadeIn");
    }
}

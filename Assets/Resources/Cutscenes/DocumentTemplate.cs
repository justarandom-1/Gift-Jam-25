using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DocumentTemplate : MonoBehaviour
{
    [SerializeField]
    private UIDocument document;

    private VisualElement root;
    

    void Start()
    {
        StartCoroutine(Generate());
        fadeIn();
    }

    void OnValidate()
    {
        if(Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    void OnEnable()
    {
        ToolkitDocumentManager.fadeOut+=fadeOut;
        root = document.rootVisualElement;
        hideInitial();
    }

    void OnDisable()
    {
        ToolkitDocumentManager.fadeOut-=fadeOut;
    }

    private IEnumerator Generate()
    {
        yield return null;
        root.Clear();
        generateContent();

    }

    //override this method to do the funny
    protected abstract void generateContent();

    private void hideInitial()
    {
        root.style.opacity = 0;
    }
    private void fadeIn()
    {
        //fade in animation
    }

    private void fadeOut()
    {
        //fade out animation
    }


}

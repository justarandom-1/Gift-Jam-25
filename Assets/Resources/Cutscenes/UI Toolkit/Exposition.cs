using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Exposition : DocumentTemplate
{
    TypewriterLabel narration, narration2, narration3, narration4;
    VisualElement storyboardImage, storyboardImage2, storyboardImage3, storyboardImage4;
    VisualElement container;
    protected override void generateContent()
    {
        narration = Create<TypewriterLabel>("narration", "n1");
        narration2 = Create<TypewriterLabel>("narration", "n2");
        narration3 = Create<TypewriterLabel>("narration", "n3");
        narration4 = Create<TypewriterLabel>("narration", "n4");

        storyboardImage = Create("sb-image", "i1");
        storyboardImage2 = Create("sb-image", "i2");
        storyboardImage3 = Create("sb-image", "i3");
        storyboardImage4 = Create("sb-image", "i4");
        container = Create("container");

        root.Add(container);
        container.Add(narration);
        container.Add(storyboardImage);
        container.Add(narration2);
        container.Add(storyboardImage2);
        container.Add(narration3);
        container.Add(storyboardImage3);
        container.Add(narration4);
        container.Add(storyboardImage4);


        if(Application.isPlaying)
        {
            narration.setNewText("Now you may be wondering how we got to this point");
            StartCoroutine(narration.autoIncrement(animationOne));
        }
    }

    protected override void nextSceneRequested()
    {
        throw new System.NotImplementedException();
    }

    public void zoomOut(Action onCompleted)
    {
        container.experimental.animation.Start(10, 5, 500, (e, v) => e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f)))).OnCompleted(onCompleted);
    }
   
    public void animationOne()
    {
        zoomOut(animationTwo);
    }
    public void animationTwo()
    {
        container.experimental.animation.Start(0, -960, 500, (e,v)=> e.style.left = v).OnCompleted(animationThree);
    }
    public void animationThree()
    {
        container.experimental.animation.Start(5, 10, 500, (e, v) => {e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f))); e.style.left = -960 + (((v-5)/10)*-960);}).OnCompleted(animationFour); 
    }
    public void animationFour()
    {
        StartCoroutine(narration2.autoIncrement(animationFive));
    }
    public void animationFive()
    {

    }
}

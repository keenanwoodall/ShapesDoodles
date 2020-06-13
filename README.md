# Shapes Sketches
Sketches done with the wonderful [Shapes library](https://acegikmo.com/shapes) for Unity.


Comes with a helpful `Sketch.cs` base component which you can inherit from to see your sketches in edit mode.

After inheriting from the Sketch class, you can override the `Setup`, `Cleanup`, `Step` and `Render` functions to create your sketch.
Plop your component on any object, and it will run while you have it selected in the inspector.'

```cs
public class SomeSketch : Sketch
{
  public override void Render()
  {
    Draw.X(...);
  }
}
```

![0](https://i.imgur.com/be6Pj7H.gif)

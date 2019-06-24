# ShirleyTracerSharp
A(nother) set of C# ports of Peter Shirley's short books on raytracing: In One Weekend, The Next Week, and The Rest Of Your Life

Included are some nice QoL features that go beyond the scope of the books, including:
- Storage of each of the demo scenes from the books for easy access (see SceneGenerator.cs)
- Optional parallel processing switch (see RenderConstants.cs)
- Optional render data overlay at render time, including render size, samples per pixel, whether it was rendered in parallel, and total render time (see RenderConstansts.cs)

# InOneWeekend
![InOneWeekend Render Example](https://github.com/SCLDGit/ShirleyTracerSharp/blob/master/InOneWeekend/InOneWeekend/ExampleImage/ExampleRender.png)

# Changelog

- 6/24/2019 - Initial commit.
- 6/24/2019 - Added Apache 2.0 license for ImageSharp.
- 6/24/2019 - Some minor code cleanup.
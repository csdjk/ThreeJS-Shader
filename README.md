# ThreeJS-Shader
基于WebGL的Three.js 的Shader，记录学习GLSL过程中的一些特效demo.

可以直接阅读源码, 里面有详细注释, 从最简单的绘制线条到各种炫酷的特效,逐渐深入学习Shader！

## 001_Hello World：第一个shader程序！

第一个WebGL shader程序！使用three.js简单构建了一个基于WebGL的shader程序, 可以直接在浏览器运行预览，可以看到整个屏幕呈现红色的闪烁效果。

## 002_gradual：渐变色

在开始这个程序之前，我封装了一个Shader Manager模块用来管理Shader，以便于后续的Shader编写。    
这个shader也比较简单，根据uv坐标显示不同的颜色，来达到渐变效果。

## 003_line：绘制了直线，曲线。
该shader展示了如何绘制线条，如直线、smoothstep曲线、正弦曲线等等。




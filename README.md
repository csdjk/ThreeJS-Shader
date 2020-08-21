# ThreeJS-Shader
基于WebGL的Three.js 的Shader，记录学习GLSL过程中的一些特效demo.

可以直接阅读源码, 里面有详细注释, 从最简单的绘制线条到各种炫酷的特效,逐渐深入学习Shader！

如果直接运行index.html 文件出现跨域问题需自行处理。

## 001_Hello World：第一个shader程序！

第一个WebGL shader程序！使用three.js简单构建了一个基于WebGL的shader程序, 可以直接在浏览器运行预览，可以看到整个屏幕呈现红色的闪烁效果。

![quicker_d7956ac8-594d-4935-80e8-7090a90bb0b4.png](https://i.loli.net/2020/08/21/ktAd6waycTUnQvD.png)

## 002_gradual：渐变色

在开始这个程序之前，我封装了一个Shader Manager模块用来管理Shader，以便于后续的Shader编写。    
这个shader也比较简单，根据uv坐标显示不同的颜色，来达到渐变效果。

![quicker_fc0e319b-cf1b-4fe3-8e95-ef01746d79d9.png](https://i.loli.net/2020/08/21/T7VYXe6CGQrNijW.png)

## 003_line：绘制了直线，曲线。
该shader展示了如何绘制线条，如直线、smoothstep曲线、正弦曲线等等。

![quicker_52ea6fa3-c3e5-42d9-a1c0-310482407753.png](https://i.loli.net/2020/08/21/rJAUwdcmvEi4VSP.png)

## 004_square：绘制正方形。
该shader展示了如何绘制一个彩色正方形。

![quicker_71aaa85e-2de8-4c95-970d-f246de9b7bdf.png](https://i.loli.net/2020/08/21/WDe2RHh5NLvjwiC.png)

## 005_circle：绘制圆形。
该shader展示了如何绘制圆形,并且通过不同的方式叠加造成一些奇特的特效。例如：绘制多个圆，两个圆相吸、融合效果，两圆相交（可以用该思路实现遮罩效果）等效果。

![quicker_d5a3c346-c76d-43d0-a30a-71391b9791ff.png](https://i.loli.net/2020/08/21/NpAlj8ukFJi5tdT.png)

## 006_drawGraph：绘制各种图案。
结合不同的【造型函数】绘制各式各样的图案,这里展示了圆、圆环、花瓣、齿轮、风车、水滴等图形，并且可以结合时间变量【u_time】使其运动。
可以自己拓展【造型函数】来绘制自己想要的图案。

![graph.gif](https://i.loli.net/2020/08/21/7ELziWqHIgTwuOm.gif)

## 007_SharpEdgesAura：尖刃光环

![quicker_862bb5a4-16de-4c32-ae6a-fd2ec345238b.png](https://i.loli.net/2020/08/21/Zv3D6hJXQPzTKM1.png)

## 008_fancyGraph：结合自定义造型函数绘制炫丽的图形。
一个小练习,绘制了一个不断变换的图形.

![fancy.gif](https://i.loli.net/2020/08/21/khzuIastUYMZgDp.gif)

## 009_fancyGear：炫丽的小齿轮。
一个小练习,绘制了一些不断变换运动的彩色小齿轮.

![gear.gif](https://i.loli.net/2020/08/21/gyp4CfoLOSjxsvH.gif)

## 010_matrix: 变换矩阵的运用

旋转：

![matrix.gif](https://i.loli.net/2020/08/21/fKBnOmXSNchxlGu.gif)

## 011_splitGrid：切割/平铺图案。

![quicker_a61af6d8-a3eb-4869-b2d9-88a39129f950.png](https://i.loli.net/2020/08/21/TejqSrUxHb5QI3D.png)

## 012_splitGrid_animation：平铺图案一个动画demo。

![splitGrid.gif](https://i.loli.net/2020/08/21/tRlQMgvDbcpBdez.gif)

## 013_noise: 噪声的应用。

![noise.gif](https://i.loli.net/2020/08/21/8vglD4UNtfoBSTJ.gif)

## 014_ripple：波纹动画

![ripple.gif](https://i.loli.net/2020/08/21/htmTLBDz3ZwAoej.gif)

## 015_floridRipple: 炫丽的波纹 + 噪声

![florid.gif](https://i.loli.net/2020/08/21/bQAqHBows7ykDVv.gif)

## 016_cell: 细胞（距离场的运用）

![cell.gif](https://i.loli.net/2020/08/21/5wYfmljr3yuMndI.gif)

## 017_distanceField_2D: 桃心函数

![quicker_3a3e5446-8dbd-47fb-b247-94fef82ae667.png](https://i.loli.net/2020/08/21/OM81xpmD26nXhTw.png)

## 018_rayMarching: 光线行进 绘制 3D 物体

![3Dbox.gif](https://i.loli.net/2020/08/21/WzX9EYKUnOCGPLd.gif)

## 019_sea2D: 2D 海洋

![sea.gif](https://i.loli.net/2020/08/21/yR35WwkshUHQLt9.gif)
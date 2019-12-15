// create by 长生但酒狂
// create time : 2019-12-13 22:00
// ------------------------------【片元着色器】----------------------------
// GLSH 绘制直线 曲线

#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕大小
uniform vec2 u_resolution;

void main() {
	// gl_FragCoord: 当前像素坐标
	// 对坐标进行规范化，把坐标控制在[0,1]范围.
	vec2 uv = gl_FragCoord.xy/u_resolution;
	// 线宽度
	float lineWidth = 0.01;
	//直线方程
	// float y = 1.0 * uv.x + 0.0;

	// smoothstep曲线
	float y = smoothstep(0.0 , 1.0,uv.x );


	float col = step(lineWidth, abs(y-uv.y));
	gl_FragColor = vec4(vec3(col),1);
}
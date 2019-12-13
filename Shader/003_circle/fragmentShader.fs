// create by 长生但酒狂
// create time : 2019-12-13 22:00
// ------------------------------【片元着色器】----------------------------
// GLSH 画布边界点

#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕坐标
uniform vec2 u_resolution;

void main() {
	// gl_FragCoord: 当前像素坐标
	// 用 gl_FragCoord.xy 除以 u_resolution，对坐标进行了规范化。
	// 这样做是为了使所有的值都在 0.0 到 1.0 之间。
	vec2 st = gl_FragCoord.xy/u_resolution;
	//半径
	float radius = 0.05;
	//圆点中心
	vec2 center = vec2( 0.5 , 0.5 );
	// 距离
	float dis = distance(center,st);
	// -------------------【为优化版】----------------------
	// if(dis <= radius){
	// 	gl_FragColor = vec4(0,1,1,1);
	// }else{
	// 	gl_FragColor = vec4(0,0,0,1);
	// }
	// -------------------【优化后】----------------------
	float col = step(radius,dis );
	gl_FragColor = vec4(vec3(col),1);
}
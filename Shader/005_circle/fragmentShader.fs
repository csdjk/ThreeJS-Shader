// Author: 长生但酒狂
// create time : 2019-12-13 22:00
// Title：绘制圆

// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕大小
uniform vec2 u_resolution;
uniform float u_time;

vec3 colorA = vec3(0.625,1.000,0.250);
vec3 colorB = vec3(0.014,0.021,1.000);

void main() {
	// gl_FragCoord: 当前像素坐标
	// 用 gl_FragCoord.xy 除以 u_resolution，对坐标进行了规范化。
	// 这样做是为了使所有的值都在 0.0 到 1.0 之间。
	vec2 uv = gl_FragCoord.xy/u_resolution;
	//半径
	float radius = 0.2;

	// 一个圆
	float pct = distance(vec2(0.600,0.460),uv);
	//椭圆
	// pct = distance(uv,vec2(0.470,0.600)) + distance(uv,vec2(0.570,0.540));
	//两个圆相吸
    // pct = distance(uv,vec2(0.550,0.520)) * distance(uv,vec2(0.010,0.040));
	//两个圆
    // pct = min(distance(uv,vec2(0.140,0.130)),distance(uv,vec2(0.660,0.600)));
	//两个圆相交部分 - 可以用来做遮罩
    // pct = max(distance(uv,vec2(0.550,0.590)),distance(uv,vec2(0.6)));
	//不知道怎么描述
    // pct = pow(distance(uv,vec2(0.350,0.450)),distance(uv,vec2(-0.490,0.360)));

	// -----------------------------------------

	// float col = step(radius,pct );

	//  smoothstep 平滑渡过
    float col = smoothstep(radius + 0.008,radius,pct ) ;

	gl_FragColor = vec4(vec3(col),1);
}
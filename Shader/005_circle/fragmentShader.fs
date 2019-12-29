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

vec2 shakeAnimate(vec2 p,float cycle){
	 // animate
  float tt = mod(u_time, cycle) / cycle;
  float ss = 1.0 + 0.5 * sin(tt * 3.1415926 * 6.0 + p.y * 0.5) * exp(-tt * 4.0);
  vec2 res = p*vec2(0.7, 1.5) + ss * vec2(0.3, -0.5);
  return res;
}

void main() {
	// gl_FragCoord: 当前像素坐标
	// 用 gl_FragCoord.xy 除以 u_resolution，对坐标进行了规范化。
	// 这样做是为了使所有的值都在 0.0 到 1.0 之间。
	vec2 uv = gl_FragCoord.xy/u_resolution;
	//  vec2 uv = (2.0*gl_FragCoord.xy - u_resolution.xy)/u_resolution.y;
	// vec2 uv = (2.0 * gl_FragCoord.xy - u_resolution.xy) / min(u_resolution.y, u_resolution.x);

  	vec3 bcol = vec3(0.29412, 0.70196, 0.63921) * (1.0 - 0.3 * length(uv));
	//半径
	float radius = 0.2;

	// 一个圆
	float pct = distance(vec2(0.5,0.5),uv);
	// -----------可以一步一步的取消下面注释,看看效果,不同的方式叠加,造成不同的效果---------------
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

	//  smoothstep 平滑过渡
    float col = smoothstep(radius + 0.008,radius,pct ) ;

	gl_FragColor = vec4(vec3(col+bcol),1);
}
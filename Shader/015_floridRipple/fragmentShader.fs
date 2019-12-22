// Author: 长生但酒狂
// create time : 2019-12-21
// Title：noise波纹

// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec3 permute(vec3 x) { return mod(((x*34.0)+1.0)*x, 289.0); }

float snoise(vec2 v){
  const vec4 C = vec4(0.211324865405187, 0.366025403784439,
           -0.577350269189626, 0.024390243902439);
  vec2 i  = floor(v + dot(v, C.yy) );
  vec2 x0 = v -   i + dot(i, C.xx);
  vec2 i1;
  i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
  vec4 x12 = x0.xyxy + C.xxzz;
  x12.xy -= i1;
  i = mod(i, 289.0);
  vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))
  + i.x + vec3(0.0, i1.x, 1.0 ));
  vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy),
    dot(x12.zw,x12.zw)), 0.0);
  m = m*m ;
  m = m*m ;
  vec3 x = 2.0 * fract(p * C.www) - 1.0;
  vec3 h = abs(x) - 0.5;
  vec3 ox = floor(x + 0.5);
  vec3 a0 = x - ox;
  m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );
  vec3 g;
  g.x  = a0.x  * x0.x  + h.x  * x0.y;
  g.yz = a0.yz * x12.xz + h.yz * x12.yw;
  return 130.0 * dot(m, g);
}

vec3 hsv2rgb_smooth( in vec3 c )
{
    vec3 rgb = clamp( abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0, 0.0, 1.0 );

	rgb = rgb*rgb*(3.0-2.0*rgb); // cubic smoothing

	return c.z * mix( vec3(1.0), rgb, c.y);
}

// 波纹
vec3 createRipple(vec2 pos,vec2 uv){
	 vec3 col = vec3(0.);
	// 中心
    // vec2 pos = vec2(0.5,0.5);
    //波纹宽度
    float width = 0.52;
    //波纹数量
    float rippleNum = 3.;
    //扩散速度
    float speed = 1.5;
    //距离
    float dis = distance(pos,uv);
    //动画
    float action = dis * rippleNum * 5. - u_time*speed ;
    //宽度逐渐减小
    width *= (0.6-dis);
    //是否是波纹, 0 是波纹, 1 是背景
    float isRipple = smoothstep(width,width+0.05,abs(sin(action)));

 	//背景颜色
	col += vec3(dis,dis,0.) * isRipple;
    //波纹颜色
  	vec3 color = hsv2rgb_smooth(vec3(snoise(uv),1,1));
	col += color * (1. - isRipple);

    return vec3(col);
}

void main() {
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;
    uv *= 1.8;

    vec3 color1 = createRipple(vec2(0.320,0.300),uv*snoise(uv));

    vec3 col = color1 - hsv2rgb_smooth(vec3(snoise(uv)*sin(u_time),1.0,0.3));
    gl_FragColor = vec4(col,1.0);
}
// Author: 长生但酒狂
// create time : 2019-12-14 22:00
// Title：绘制正方形
// ------------------------------【片元着色器】----------------------------

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;
// hsb 转 rgb
vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0,0.0,1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix( vec3(1.0), rgb, c.y);
}

void main(){
    // uv坐标
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    // 
    vec3 color = vec3(0.);
    // 边长 - width
	float width = 0.356;
    
    // 左下两条边 -  bottom-left
    vec2 bl = step(vec2(width),uv);
    float pct = bl.x * bl.y;

    // 右上两条边 - top-right
     vec2 tr = step(vec2(width),1.0-uv);
     pct *= tr.x * tr.y;
    // 颜色根据x变化, 这里用到了hsb2rgb函数 把hsb值转换为rgb值
     color = vec3(pct) * hsb2rgb(vec3( uv.x*2., 1.,1. ));
    // 输出
    gl_FragColor = vec4(color,1.0);
}
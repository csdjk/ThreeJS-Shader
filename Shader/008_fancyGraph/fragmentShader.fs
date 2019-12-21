// Author: 长生但酒狂
// create time : 2019-12-16
// Title：练习: 结合造型函数绘制炫丽的图形

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0,0.0,1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix( vec3(1.0), rgb, c.y);
}

vec2 tile(vec2 _st, float _zoom){
    _st *= _zoom;
    return fract(_st);
}

void main()
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = gl_FragCoord.xy / u_resolution.xy;
     uv = tile(uv,3.);
    //width height ratio
    float ratio = u_resolution.x/u_resolution.y;
 	//
	uv.x = uv.x*ratio - 0.4;
  
    vec3 color = vec3(0.0);

    vec2 pos = vec2(0.5)-uv;
    float radius = length(pos)*2.0;
    float angle = atan(pos.y,pos.x);

    float f1 = fract(angle*1.376+u_time)*0.8;
	 float f2 = fract(angle*2.408+u_time)*0.8;
	 float f = f1*f2;

    color = vec3( 1.-smoothstep(f,f+0.02,radius) );
      
    color = color * hsb2rgb(vec3((f+u_time),1.,1.000));
      
    gl_FragColor = vec4(color, 1.0);
}

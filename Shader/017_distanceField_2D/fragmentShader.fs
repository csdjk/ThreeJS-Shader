// Author:
// Title:

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

float sdVesica(vec2 p, float r, float d)
{
    p = abs(p);

    float b = sqrt(r*r-d*d);  // can delay this sqrt by rewriting the comparison
    return ((p.y-b)*d > p.x*b) ? length(p-vec2(0.0,b))*sign(d)
                               : length(p-vec2(-d,0.0))-r;
}

float sdCircle( vec2 p, float r )
{
  return length(p) - r;
}


float sdSphere( vec3 p, float s )
{
  return length(p)-s;
}

float sdUnevenCapsule( vec2 p, float r1, float r2, float h )
{
    p.x = abs(p.x);
    float b = (r1-r2)/h;
    float a = sqrt(1.0-b*b);
    float k = dot(p,vec2(-b,a));
    if( k < 0.0 ) return length(p) - r1;
    if( k > a*h ) return length(p-vec2(0.0,h)) - r2;
    return dot(p, vec2(a,b) ) - r1;
}
//心型SDF
float heartSDF(vec2 point) {
	// point.y -= 0.25;
  float a = atan(point.x, point.y) / 3.141593;
  float r = length(point);
  float h = abs(a);
  float d = 0.5*(13.0 * h - 22.0 * h * h + 10.0 * h * h * h) / (6.0 - 5.0 * h);
  return r-d;
}

float sdHexagram( in vec2 p, in float r )
{
    const vec4 k = vec4(-0.5,0.8660254038,0.5773502692,1.7320508076);
    p = abs(p);
    p -= 2.0*min(dot(k.xy,p),0.0)*k.xy;
    p -= 2.0*min(dot(k.yx,p),0.0)*k.yx;
    p -= vec2(clamp(p.x,r*k.z,r*k.w),r);
    return length(p)*sign(p.y);
}
void main() {
  vec2 p = (2.0*gl_FragCoord.xy - u_resolution.xy)/u_resolution.y;
  vec3 color = vec3(0.);
  float d = sdHexagram( p + vec2(0.000,0.020), 0.1 );
  d = heartSDF(p);
  vec3 col = vec3(1.0) - sign(d);
  gl_FragColor = vec4(col,1.0);
}
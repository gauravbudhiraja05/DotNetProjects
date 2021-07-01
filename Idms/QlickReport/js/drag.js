// JScript File
var hidden=0;
var dragapproved=false
var z,x,y,l,t,h
function move(){
if (event.button==1&&dragapproved){
//var ht=parseInt(h.replace("px",""))

    z.style.pixelLeft=temp1+event.clientX-x
    z.style.pixelTop=temp2+event.clientY-y
    var s=z.offsetLeft
    var o=z.offsetWidth
   // var y=z.offsetTop
    var u=z.offsetHeight
if(s<l)
{
       z.style.pixelLeft="0"
       z.style.pixelTop="0"
}
if(s+o>l+750)
{
    z.style.pixelLeft="0"
    z.style.pixelTop="0"
}
if(z.offsetTop<t)
{
    z.style.pixelTop="0"
    z.style.pixelLeft="0"
}
if(z.offsetTop+u>t+h)
{  
   z.style.pixelTop="0"
   z.style.pixelLeft="0"
}
return false
event.srcElement=null // added on 11-10-08
}
}
function drags(){
if (!document.all)
return

if (event.srcElement.className=="drag"){
dragapproved=true
// newly added on July 24,08
var pa=event.srcElement.parentElement.id
l=getRealLeft(pa)
t=getRealTop(pa)
h=event.srcElement.parentElement.offsetHeight
z=event.srcElement
temp1=z.style.pixelLeft
temp2=z.style.pixelTop
x=event.clientX
y=event.clientY
document.onmousemove=move

}
}
function getRealLeft(el){
        var xPos = document.getElementById(el).offsetLeft;
        var tempEl = document.getElementById(el).offsetParent;
        while (tempEl != null) {
        xPos += tempEl.offsetLeft;
        tempEl = tempEl.offsetParent;
        }
      return xPos;
    }

    function getRealTop(el){
        var yPos = document.getElementById(el).offsetTop;
        var tempEl = document.getElementById(el).offsetParent;
        while (tempEl != null) {
        yPos += tempEl.offsetTop;
        tempEl = tempEl.offsetParent;
        }
        return yPos;
    }

document.onmousedown=drags
document.onmouseup=new Function("dragapproved=false")
document.onmouseout=new Function("dragapproved=false")

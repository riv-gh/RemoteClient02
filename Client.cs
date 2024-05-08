using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace RemoteClient02
{
  internal class Client
  {
    public byte[] BitmapToByte(Bitmap bmp) => this.ImageToByte((Image) bmp);

    public byte[] ImageToByte(Image img) => (byte[]) new ImageConverter().ConvertTo((object) img, typeof (byte[]));

    public Client(TcpClient Client)
    {
            try
            {
                if (false)
                //if (((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString() != "<your \"192.168.0.12\">")
                {
                    Client.Close();
                }
                else
                {
                    string input = "";
                    byte[] numArray1 = new byte[1024];
                    int count;
                    while ((count = Client.GetStream().Read(numArray1, 0, numArray1.Length)) > 0)
                    {
                        input += Encoding.ASCII.GetString(numArray1, 0, count);
                        if (input.IndexOf("\r\n\r\n") >= 0 || input.Length > 4096)
                            break;
                    }
                    Match match = Regex.Match(input, "^\\w+\\s+([^\\s\\?]+)[^\\s]*\\s+HTTP/.*|");
                    if (match == Match.Empty)
                        return;
                    string str1 = Uri.UnescapeDataString(match.Groups[1].Value);
                    if (str1 == "/screen.png")
                    {
                        //Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width * Screen.AllScreens.Length, Screen.PrimaryScreen.Bounds.Height);
                        Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                        Graphics.FromImage((Image)bmp).CopyFromScreen(0, 0, 0, 0, bmp.Size);
                        byte[] numArray2 = this.BitmapToByte(bmp);
                        string str2 = Encoding.Default.GetString(numArray2);
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-type: " + numArray2.GetContentType() + "\r\nContent-Length: " + (object)str2.Length + "\r\nConnection: close\r\n\r\n");
                        byte[] buffer = new byte[bytes.Length + numArray2.Length];
                        for (int index = 0; index < buffer.Length; ++index)
                            buffer[index] = index >= bytes.Length ? numArray2[index - bytes.Length] : bytes[index];
                        Client.GetStream().Write(buffer, 0, buffer.Length);
                    }
                    if (str1 == "/")
                    {
                        string str2 =
              @"<!DOCTYPE html> 
<html lang='en'> 
<head> 
  <link rel=icon type=image/gif href=data:image/gif;base64,R0lGODlhEAAQAIAAAAAAAAAAACH5BAkAAAEALAAAAAAQABAAAAIgjI+py+0PEQiT1lkNpppnz4HfdoEH2W1nCJRfBMfyfBQAOw== /> 
   <title>RemouteMi</title> 
  <style> 
      html, body {
        margin: 0; height: 100%;
        overflow: hidden;
      } 
      #screen_canvas {
        width: 100%;
        height: 100%;
      }
      #panel {
        
          /* display:block; */
          position:absolute;
          left: 50%;
          top: 0;
          width: 600px;
          height: 40px;
          margin-left: -300px;
          margin-top: -35px;
          background-color: white;
          border: 1px solid black;
          border-top: 0;
          border-radius: 0 0 10px 10px;
          opacity: 0.5;
          transition: opacity 0.5s, margin-top 0.5s;

          display: flex;
          flex-flow: row wrap;
          justify-content: space-around;

          padding: 0;
      }
      #panel:hover {
        opacity: 1;
        margin-top: 0px;
      }
      #panel>li {
          line-height: 40px;
          cursor: default;
          list-style: none;
          padding: 0;
          margin: 0;
          
      }
      #panel>li.clickable:hover {
          cursor: pointer;
          text-decoration: underline;
      }
      .window {
          position:absolute;
          width: 230px;
          height: 44px;
          margin-top: -35px;
          background-color: white;
          border: 1px solid black;
          border-radius: 10px;
          opacity: 0.5;
          left:300px;
          top: 300px;
          transition: opacity 0.2s;
          padding: 5px;
          cursor: default;
      }
      .window:hover {
          opacity: 1;
      }
      .window>.caption {
        cursor: move;
      }
      .hide {
        display: none;
      }
  </style> 
</head> 
<body oncontextmenu=""return false;""> 
      <ul id=""panel"">
          <!-- <li class=""clickable"" onclick=""document.querySelector('#typeWindow').classList.toggle('hide');"">TextToType</li> -->
          <li>
            <label for=""renewDelayEl"">renewDelay</label><input id=""renewDelayEl"" type=""range"" min=""1"" max=""1000"" value=""200"">
          </li>
          <li class=""clickable"" onclick=""window.open('/about/');"">
            About          
          </li>
      </ul>
      <div class=""window hide"" id=""typeWindow"">
          <div class=""caption"" id=""typeWindowHeader"" >
              <span>send to type</span>
          </div>
          <input type=""text"" id=""toTypeText"">
          <input type=""button"" value=""send"" onclick=""sendData(`/event/keyboard/${document.querySelector('#toTypeText').value}`);"">
      </div>
      <canvas
        id=""screen_canvas""
        width=""1024""
        height=""768""
        onmousedown=""sendData(`/event/mouse/mdown/${cursorX}/${cursorY}/`);""
        onmouseup=""sendData(`/event/mouse/mup/${cursorX}/${cursorY}/`);""
        oncontextmenu=""sendData(`/event/mouse/rclick/${cursorX}/${cursorY}/`);""
      >
      </canvas>
      <input class=""hide0"" id=""typerInput"" type=""text"" style=""position: absolute; top:-100px; left: -100px;"">
      <script>
        const renewDelayEl = document.getElementById('renewDelayEl');
        let renewDelay;
        renewDelayEl.addEventListener('mousemove', function(e) { //cange
          renewDelay = event.target.value;
          let tmp = event.target.labels[0].innerHTML.split(':')[0];
          event.target.labels[0].innerHTML = `${tmp}:${renewDelay}ms`;
          setCookie('renew_delay', event.target.value);
        });
        renewDelayEl.value = getCookie('renew_delay') || 200;
        renewDelayEl.dispatchEvent(new Event('change', {bubbles : true, cancelable : true}));
        


        
        const canvas = document.querySelector('canvas');
        const ctx = canvas.getContext('2d');
        let cursorX = 0, cursorY = 0;
        let img = new Image();

        function renewImage () {
          img.src = getLink();
          img.onload = function(){
              canvas.width = img.naturalWidth;
              canvas.height = img.naturalHeight;
              ctx.drawImage(img, 0, 0, canvas.width, canvas.height); // drawImage(img, x, y);
              setTimeout(renewImage, renewDelay);
          }
          function getLink() {
            return `/screen.png?${Math.random()}`;
          }
        }
        renewImage()

        canvas.addEventListener('mousemove', function (e) { 
            let zx = parseFloat(window.getComputedStyle(canvas).width)/canvas.width;
            let zy = parseFloat(window.getComputedStyle(canvas).height)/canvas.height;
            cursorX = Math.round(e.pageX/zx - e.target.offsetLeft);
            cursorY = Math.round(e.pageY/zy - e.target.offsetTop);
            sendData(`/event/mouse/move/${cursorX}/${cursorY}/`);
        });


        document.querySelectorAll('.window').forEach((el)=>{
            dragElement(el);
        });
        
        function dragElement(elmnt) {
          let pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
          if (document.getElementById(elmnt.id + ""Header"")) {
            document.getElementById(elmnt.id + ""Header"").onmousedown = dragMouseDown;
          } else {
            elmnt.onmousedown = dragMouseDown;
          }

          function dragMouseDown(e) {
            e = e || window.event;
            e.preventDefault();
            pos3 = e.clientX;
            pos4 = e.clientY;
            document.onmouseup = closeDragElement;
            document.onmousemove = elementDrag;
          }

          function elementDrag(e) {
            e = e || window.event;
            e.preventDefault();
            pos1 = pos3 - e.clientX;
            pos2 = pos4 - e.clientY;
            pos3 = e.clientX;
            pos4 = e.clientY;
            elmnt.style.top = (elmnt.offsetTop + 35 - pos2) + ""px""; //panel margin top !!!!
            elmnt.style.left = (elmnt.offsetLeft - pos1) + ""px"";
            
          }

          function closeDragElement() {
            document.onmouseup = null;
            document.onmousemove = null;
          }
        }


        
        function sendData(requestStr) {
          let xhr = new XMLHttpRequest(); 
          xhr.open('GET', requestStr, true);
          xhr.send();
        }

        function setCookie(cname, cvalue, exdays) {
          var d = new Date();
          d.setTime(d.getTime() + (exdays*24*60*60*1000));
          var expires = ""expires=""+ d.toUTCString();
          document.cookie = cname + ""="" + cvalue + "";"" + expires + "";path=/"";
        }

        function getCookie(cname) {
          var name = cname + ""="";
          var decodedCookie = decodeURIComponent(document.cookie);
          var ca = decodedCookie.split(';');
          for(var i = 0; i <ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
              c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
              return c.substring(name.length, c.length);
            }
          }
          return """"
        }


        const typerInputEl = document.querySelector('#typerInput')
        document.querySelector('body').addEventListener(""keydown"", event => {
          if(
            true//['input', 'textarea'].indexOf(event.target.tagName.toLowerCase()) == -1
          ) {
            //sendData(`/event/keyboard/${String.fromCharCode(event.keyCode).toLocaleLowerCase()}`);
            typerInputEl.focus();
          }
        });
        document.querySelector('body').addEventListener(""keyup"", event => {
          if(
            true//['input', 'textarea'].indexOf(event.target.tagName.toLowerCase()) == -1
          ) {
            sendData('/event/keyboard/'+eventKeyF(event.key, typerInputEl.value))
            console.log(event.key)
            typerInputEl.value='';
          }
        });

        function eventKeyF(eventKey, input) {
            const keyMap = new Map([
              ['Enter', '{ENTER}'],
              ['Backspace', '{BACKSPACE}'],
              ['Delete', '{DELETE}'],
              ['ArrowDown', '{DOWN}'],
              ['ArrowUp', '{UP}'],
              ['ArrowLeft', '{LEFT}'],
              ['ArrowRight', '{RIGHT}'],
              ['PageUp', '{PGUP}'],
              ['PageDown', '{PGDN}'],
              ['Home', '{HOME}'],
              ['End', '{END}'],
              ['Insert', '{INSERT}']
            ]);
            return keyMap.get(eventKey) || input;
        }

      </script>
  </body> 
</html> 

";
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-type: text/html\r\nContent-Length: " + (object)str2.Length + "\r\nConnection: close\r\n\r\n" + str2);
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                    }
                    if (str1 == "/test/")
                    {
                        string str2 = System.IO.File.ReadAllText("test.html", Encoding.Default);
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-type: text/html\r\nContent-Length: " + (object)str2.Length + "\r\nConnection: close\r\n\r\n" + str2);
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                    }
                    if (str1 == "/about/")
                    {
                        string str2 = "<!DOCTYPE html> \r\n<html lang='en'> \r\n  <head> \r\n    <link rel=icon type=image/gif href=data:image/gif;base64,R0lGODlhEAAQAIAAAAAAAAAAACH5BAkAAAEALAAAAAAQABAAAAIgjI+py+0PEQiT1lkNpppnz4HfdoEH2W1nCJRfBMfyfBQAOw== /> \r\n     <title>RemouteMi</title> \r\n    <style> \r\n        html,body, \r\n        img { \r\n                display: block; \r\n                /*max-width: 100%; \r\n                max-height: 100%;*/ \r\n                padding: 0; \r\n                margin: 0; \r\n            } \r\n    </style> \r\n  </head> \r\n  <body> \r\n     <h1>RemoteClient02</h1> \r\n     <a href=\"https://github.com/riv-gh/RemoteClient02\">Page on GitHub</a>\r\n  </body> \r\n</html> \r\n";
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-type: text/html\r\nContent-Length: " + (object)str2.Length + "\r\nConnection: close\r\n\r\n" + str2);
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                    }
                    if (str1.IndexOf("/event/clipboard/") != -1)
                    {
                        string clipboard = str1.Replace("/event/clipboard/", "");
                        Clipboard.SetText(clipboard);
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n");
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                    }
                    if (str1.IndexOf("/event/keyboard/") != -1)
                    {
                        string text = str1.Replace("/event/keyboard/", "");
                        if (text == "")
                            text = " ";
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n");
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                        SendKeys.SendWait(text);
                    }
                    if (str1.IndexOf("/event/mouse/") != -1)
                    {
                        string[] strArray = str1.Split('/');
                        Point position = Cursor.Position;
                        Cursor.Position = new Point(Convert.ToInt32(strArray[4]), Convert.ToInt32(strArray[5]));


                        if (strArray[3] == "click")
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                            Thread.Sleep(5);
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        }
                        if (strArray[3] == "rclick")
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
                            Thread.Sleep(5);
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
                        }
                        if (strArray[3] == "mdown")
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        }
                        if (strArray[3] == "mup")
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        }
                        if (strArray[3] == "mrdown")
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
                        }
                        if (strArray[3] == "mrup")
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        }
                        //Thread.Sleep(5);
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n");
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                    }
                    if (str1.IndexOf("/event/key/") != -1)
                    {
                        string[] strArray = str1.Split('/');
                        string str2 = "<html><body><h1>" + strArray[3].ToString() + " - " + strArray[3] + "</h1></body></html>";
                        byte[] bytes = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-type: text/html\r\nContent-Length:" + str2.Length.ToString() + "\r\n\r\n" + str2);
                        Client.GetStream().Write(bytes, 0, bytes.Length);
                        Client.Close();
                    }
                    else
                        Client.Close();
                }
            }
            catch (Exception exc) { MessageBox.Show("Something went wrong(");  }
    }
  }
}

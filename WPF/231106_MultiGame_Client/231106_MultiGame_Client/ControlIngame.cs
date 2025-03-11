using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Timer = System.Timers.Timer;

namespace _231106_MultiGame_Client
{
    public partial class ControlIngame : UserControl
    {

        public ControlIngame()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;

            inputTimer = new Timer(1024f / 60f);
            inputTimer.Elapsed += InputProcess;
            inputTimer.Start();

            drawEngine = new DrawEngine(this, ClientController.instance.formMain);
        }
        public DrawEngine drawEngine;
        public Timer inputTimer;
        public SoundManager soundManager = new SoundManager();

        private void buttonGetOut_Click(object sender, EventArgs e)
        {
            ClientController.instance.ServerSend(Packet.RoomGetOut(ClientController.id));
        }

        private void InputProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool aPressed = (GetAsyncKeyState(VK_A) & 0x8000) != 0;
            bool sPressed = (GetAsyncKeyState(VK_S) & 0x8000) != 0;
            bool dPressed = (GetAsyncKeyState(VK_D) & 0x8000) != 0;
            bool wPressed = (GetAsyncKeyState(VK_W) & 0x8000) != 0;
            bool leftButtonPressed = (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;

            int upAxis = (wPressed ? 1 : 0) + (sPressed ? -1 : 0);
            int sideAxis = (dPressed ? 1 : 0) + (aPressed ? -1 : 0);

            Vector2 centorP = new Vector2(FormMain.winSize.X/2f, FormMain.winSize.Y/2f);
            Vector2 targetP = new Vector2(Cursor.Position.X, Cursor.Position.Y);

            float dir = Library.MakeDirection(centorP, targetP);

            string packet = Packet.IngameInput(upAxis, sideAxis, dir, leftButtonPressed);
            ClientController.instance.ServerSend(packet);
        }


        string FindSound(SoundType soundType)
        {
            string soundPath = "";
            switch (soundType)
            {
                case SoundType.SCREAM:
                    {
                        soundPath = "SFX\\Scream.wav";
                    }
                    break;
                case SoundType.HITTED:
                    {
                        soundPath = "SFX\\Hitted.wav";
                    }
                    break;
                case SoundType.FIRE_HG:
                    {
                        soundPath = "SFX\\FireHg.wav";
                    }
                    break;
                case SoundType.FIRE_SMG:
                    {
                        soundPath = "SFX\\FireHg.wav";
                    }
                    break;
                case SoundType.FIRE_AR:
                    {
                        soundPath = "SFX\\FireAr.wav";
                    }
                    break;
                case SoundType.FIRE_SG:
                    {
                        soundPath = "SFX\\FireSg.wav";
                    }
                    break;
                case SoundType.FIRE_DMR:
                    {
                        soundPath = "SFX\\FireDmr.wav";
                    }
                    break;
            }
            return soundPath;
        }

        public void PlaySound(SoundType soundType, Vector2 position)
        {
            string soundPath = FindSound(soundType);

            float volume;
            Vector2 thisPos = drawEngine.camera.position;
            float dist = (thisPos - position).Length();

            volume = (float)Math.Pow(dist / 100f, 1.5f) * 100f;
            volume = volume > 1f ? 1f : volume;

            if (soundPath == "") throw new Exception("PlaySound - " + soundPath + " >> 적절한 경로가 아닙니다.");
            soundManager.SoundPlay(soundPath, volume);

        }

        public void ShakeCamera(float strength)
        {
            drawEngine.camera.shakeValue += strength;
        }
        // Windows API 함수
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        // Virtual key codes
        const int VK_A = 0x41;
        const int VK_S = 0x53;
        const int VK_D = 0x44;
        const int VK_W = 0x57;
        const int VK_LBUTTON = 0x01;
    }
    
    public class DrawEngine : IDisposable
    {
        public DrawEngine(ControlIngame self, FormMain formMain)
        {
            ingame = self;
            master = formMain;
            formMain.Paint += PaintEvent;
            self.Paint += PaintEvent;

            camera = new Camera(self.inputTimer);
            drawUnits = new List<DrawUnit>();
            drawMessages = new List<object[]>();
        }

        ControlIngame ingame;
        FormMain master;
        public Camera camera;
        List<DrawUnit> drawUnits;
        List<object[]> drawMessages;
        float msgAlpha = 0f;

        public void DrawAll(Graphics g)
        {
            ingame.labelUpper.Text = drawUnits.Count.ToString();
            try
            {
                foreach (var unit in drawUnits)
                {
                    unit?.DrawSelf(g, camera, unit.position, unit.direction);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("DrawAll - " + ex.Message);
            }
        }
        public void DrawMessages(Graphics g) 
        {
            msgAlpha = msgAlpha < 0f? 0f : msgAlpha - 0.01f;

            for (int c = 0; c < drawMessages.Count; c++) 
            {
                object[] msg = drawMessages[c];

                Color color;

                switch ((MessageType)msg[1])
                {
                    case MessageType.USER: color = Color.DarkGray; break;
                    case MessageType.MANAGER: color = Color.Azure; break;
                    case MessageType.WARNING: color = Color.Red; break; 
                    default: color = Color.White; break;
                }
                color = Color.FromArgb( (Byte)Math.Round(255f* msgAlpha), color.R, color.G, color.B);
                
                g.DrawString((string)msg[0], new Font("돋움", 10), new SolidBrush(color), 10f, FormMain.winSize.Y - (c+1) * 12f);
            }
        }

        private void PaintEvent(object sender, PaintEventArgs e)
        {
            DrawAll(e.Graphics);
            DrawMessages(e.Graphics);

            DrawUnit target = drawUnits.Find(unit =>
            {
                if (unit is Character cha)
                    if (cha.name == ClientController.id)
                        return true;
                return false;
            });

            if(target != null)
                camera.SetTarget(target.position);

        }

        public void LoadDraw(string packet)
        {
            try
            {
                drawUnits.Clear();
                string[] drawUnitUnion = packet.Split('!');

                string[] gamerUnion = drawUnitUnion[0].Split('@');
                foreach (string unit in gamerUnion)
                {
                    if (unit == null || unit.Length == 0) continue;
                    string[] part = unit.Split('#');

                    string name = part[0];
                    bool isRedSide = bool.Parse(part[1]);
                    bool isActivated = bool.Parse(part[2]);
                    float positionX = float.Parse(part[3]);
                    float positionY = float.Parse(part[4]);
                    float direction = float.Parse(part[5]);

                    Character character = new Character(name, isActivated, isRedSide ? SideType.RED : SideType.BLUE, new Vector2(positionX, positionY), direction);
                    drawUnits.Add(character);
                }
                string[] weaponUnion = drawUnitUnion[1].Split('@');
                foreach (string unit in weaponUnion)
                {
                    if (unit == null || unit.Length == 0) continue;
                    string[] part = unit.Split('#');

                    WeaponType wType = (WeaponType)int.Parse(part[0]);
                    float positionX = float.Parse(part[1]);
                    float positionY = float.Parse(part[2]);
                    float direction = float.Parse(part[3]);

                    Weapon weapon = new Weapon(wType, new Vector2(positionX, positionY), direction);
                    drawUnits.Add(weapon);
                }
                string[] projUnion = drawUnitUnion[2].Split('@');
                foreach (string unit in projUnion)
                {
                    if (unit == null || unit.Length == 0) continue;
                    string[] part = unit.Split('#');

                    float positionX = float.Parse(part[0]);
                    float positionY = float.Parse(part[1]);
                    float direction = float.Parse(part[2]);

                    Projectile proj = new Projectile(new Vector2(positionX, positionY), direction);
                    drawUnits.Add(proj);
                }
                string[] wallUnion = drawUnitUnion[3].Split('@');
                foreach (string unit in wallUnion)
                {
                    if (unit == null || unit.Length == 0) continue;
                    string[] part = unit.Split('#');

                    float positionX = float.Parse(part[0]);
                    float positionY = float.Parse(part[1]);
                    float sizeX = float.Parse(part[2]);
                    float sizeY = float.Parse(part[3]);

                    Wall wall = new Wall(new Vector2(sizeX, sizeY), new Vector2(positionX, positionY), 0f);
                    drawUnits.Add(wall);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("LoadDraw - " +ex.Message +"\r\n"+ ex.StackTrace +"\r\n"+ packet);
            }
        }
        public void LoadMessage(string msg)
        {
            msgAlpha = 1f;

            string[] sp = msg.Split('#');

            MessageType type = (MessageType)int.Parse(sp[0]);
            string sender = sp[1];
            string message = sp[2];


            drawMessages.Add(new object[] { $"[{DateTime.Now.ToString("HH:mm:ss")}] {sender} : {message}", type });
        }

        ~DrawEngine() { Dispose(); }
        public void Dispose()
        {
            camera = null;
            drawUnits.Clear();
            drawMessages.Clear();
        }
    }

    public class Camera
    {
        public Camera(Timer timer)
        {
            timer.Elapsed += CameraTragging;
        }

        public void SetTarget(Vector2 target)
        {
            positionTarget = target;
        }

        public Vector2 screenSize = FormMain.winSize;

        public Vector2 position = Vector2.Zero;
        public Vector2 positionTarget = Vector2.Zero;
        public Vector2 shakenValue = Vector2.Zero;

        public float cameraSpeed = 0.2f;
        public float size = 1f;

        public float shakeValue = 0f;
        public float shakeSpeed = 0.4f;

        private void CameraTragging(object sender, System.Timers.ElapsedEventArgs e)
        {
            position.X = (position.X + positionTarget.X * cameraSpeed) / (1f + cameraSpeed);
            position.Y = (position.Y + positionTarget.Y * cameraSpeed) / (1f + cameraSpeed);

            shakeValue = (shakeValue + 0 * shakeSpeed) / (1f + shakeSpeed);

            Random random = new Random();
            shakenValue = MathHelper.GetVectorByAngle((float)random.NextDouble() * 360f) * (float)random.NextDouble() * shakeValue;
        }
    }

    public abstract class DrawUnit
    {
        public Vector2 position;
        public float direction;

        public DrawUnit(Vector2 position, float direction)
        {
            this.position = position;
            this.direction = direction;
        }

        public Vector2 GetRelativePosition(Camera camera, Vector2 rawPos)
        {
            Vector2 retPos = rawPos;

            retPos += camera.screenSize / 2f - (camera.position + camera.shakenValue);
            retPos /= camera.size;

            return retPos;
        }
        public abstract void DrawSelf(Graphics g, Camera camera, Vector2 postion, float direction);
    }

    public enum SideType 
    {
        RED,
        BLUE,
    }
    public class Character : DrawUnit
    {
        public string name;
        public SideType side;
        public bool isActivated;

        public Character(string name, bool isActivated, SideType side, Vector2 position, float direction) : base(position, direction) 
        {
            this.name = name;
            this.side = side;
        }
        public override void DrawSelf(Graphics g, Camera camera, Vector2 postion, float direction)
        {
            Vector2 pos = GetRelativePosition(camera, position);

            Color color = isActivated ? side == SideType.RED ? Color.Red : Color.Blue : Color.Gray;

            int size = 40;
            g.FillEllipse(new SolidBrush(color), pos.X - size / 2, pos.Y - size / 2, size, size);
            g.DrawEllipse(
                new Pen(new SolidBrush(Color.Black)),
                new Rectangle(new Point((int)pos.X - (int)size/ 2, (int)pos.Y - (int)size / 2), new Size(size, size))
                );
            g.DrawString(name, new Font("굴림", 20), new SolidBrush(Color.Black), new Point((int)pos.X - name.Length * 2, (int)pos.Y - 30));
            //g.DrawString(postion.ToString(), new Font("돋움", 20), new SolidBrush(Color.Black), pos.X, pos.Y);
        }
    }


    public enum WeaponType
    {
        PISTOL,     //단발식 권총입니다. 느리지만 정확도와 피해량이 높습니다. 
        SMG,            //빠른 기관단총입니다. 빠른 연사력으로 근접전에서 강합니다.
        ASSAULT_RIFLE,        //돌격소총입니다. 모든 면에서 제값을 하는 총기힙니다.
        DMR,            //단발식 라이플입니다. 연사력이 낮지만 높은 피해량과 사거리를 자랑합니다.
        SHOTGUN,            //펌프 액션 산탄총입니다. 한번에 여러 탄환을 발사해 근접전에서 파괴적인 피해를 줄 수 있습니다.

    }
    public class Weapon : DrawUnit
    {
        public WeaponType wType;

        static Dictionary<WeaponType, Image> sprite;
        static Dictionary<WeaponType, Vector2> offset;
        static Weapon()
        {
            Func<string, string> func = (rel) => { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rel); };

            sprite = new Dictionary<WeaponType, Image>();
            offset = new Dictionary<WeaponType, Vector2>();

            sprite[WeaponType.PISTOL] = Image.FromFile(func("Image\\ImageHG.png"));
            offset[WeaponType.PISTOL] = new Vector2(70f, 65f);

            sprite[WeaponType.SMG] = Image.FromFile(func("Image\\ImageSMG.png"));
            offset[WeaponType.SMG] = new Vector2(90f, 65f);

            sprite[WeaponType.ASSAULT_RIFLE] = Image.FromFile(func("Image\\ImageAR.png"));
            offset[WeaponType.ASSAULT_RIFLE] = new Vector2(90f, 65f);

            sprite[WeaponType.DMR] = Image.FromFile(func("Image\\ImageDMR.png"));
            offset[WeaponType.DMR] = new Vector2(110f, 60f);

            sprite[WeaponType.SHOTGUN] = Image.FromFile(func("Image\\ImageSG.png"));
            offset[WeaponType.SHOTGUN] = new Vector2(110f, 50f);
        }
        public Weapon(WeaponType wType, Vector2 position, float direction) : base(position, direction)
        {
            this.wType = wType;
        }
        public override void DrawSelf(Graphics g, Camera camera, Vector2 postion, float direction)
        {
            Vector2 pos = GetRelativePosition(camera, position);
            try
            {
                float basicScale = 0.3f;
                Vector2 offSet = offset[wType];
                Library.DrawRotatedImage(
                    g, sprite[wType],
                    direction, pos - offSet, basicScale / camera.size);
                //g.DrawString(postion.ToString(), new Font("돋움", 20), new SolidBrush(Color.Black), pos.X, pos.Y);
            }
            catch(Exception e) { MessageBox.Show(e.Message + wType); }
            }
    }
    
    public class Wall : DrawUnit
    {
        Vector2 size;
        public Wall(Vector2 size, Vector2 position, float direction) : base(position, direction)
        {
            this.size = size;
        }
        public override void DrawSelf(Graphics g, Camera camera, Vector2 postion, float direction)
        {
            Vector2 pos = GetRelativePosition(camera, position);
            Rectangle rect = new Rectangle(new Point((int)pos.X - (int)size.X / 2, (int)pos.Y - (int)size.Y / 2), new Size((int)size.X, (int)size.Y));


            g.FillRectangle(new SolidBrush(Color.DarkSlateGray), rect);
            g.DrawRectangle(new Pen(new SolidBrush( Color.Black)), rect);
            //g.DrawString(postion.ToString(), new Font("돋움", 20), new SolidBrush(Color.Black), pos.X, pos.Y);
        }
    }
    
    public class Projectile : DrawUnit
    {
        public Projectile(Vector2 position, float direction) : base(position, direction)
        {
        }
        public override void DrawSelf(Graphics g, Camera camera, Vector2 postion, float direction)
        {
            Vector2 pos = GetRelativePosition(camera, position);
            Vector2 tailPos = pos + MathHelper.GetVectorByAngle(direction + 180f) * 30f;
            g.DrawLine(
                new Pen(new SolidBrush(Color.Black)),
                new PointF(pos.X, pos.Y),
                new PointF(tailPos.X, tailPos.Y)
                );
            //g.DrawString(postion.ToString(), new Font("돋움", 20), new SolidBrush(Color.Black), pos.X, pos.Y);
        }
    }




}

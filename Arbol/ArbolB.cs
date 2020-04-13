using System;
using System.Collections.Generic;
using System.Drawing;

namespace LFA_Proyecto.Arbol
{
    class ArbolB
    {
        public string Dato { get; set; }
        public ArbolB HijoDerecho { get; set; }
        public ArbolB HijoIzquierdo { get; set; }
        public object Root { get; internal set; }

        public int Value;
        public string First;
        public string Last;
        public string Follow;
        public bool Nuller;//? and *

        #region DrawTree
        public bool IsSingle { get { return HijoDerecho == null && HijoIzquierdo == null; } }

        private static Bitmap _NodoBolsa = new Bitmap(30, 25);
        private static Size _LiberarEspacio = new Size(_NodoBolsa.Width / 8, (int)(_NodoBolsa.Height * 1.3f));
        private static readonly float Coef = _NodoBolsa.Width / 40f;
        Image _Imagen;
        private int _InicioImagenNodo;
        private static Font font = new Font("Tahoma", 14f * Coef);
        private bool _Cambio = true;
        #endregion
        public bool EsCambio
        {
            get
            {
                if (_Cambio)
                    return true;
                var childsChanged = false;
                if (HijoIzquierdo != null)
                    childsChanged |= HijoIzquierdo.EsCambio;
                if (HijoDerecho != null)
                    childsChanged |= HijoDerecho.EsCambio;
                return childsChanged;
            }
            private set { _Cambio = value; }
        }
        static ArbolB()
        {
            var g = Graphics.FromImage(_NodoBolsa);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var rc1 = new Rectangle(1, 1, _NodoBolsa.Width - 2, _NodoBolsa.Height - 2);
            g.DrawEllipse(new Pen(Color.LightBlue, 1.0f), rc1);
        }
        public Image Draw(out int Centro)
        {
            Centro = _InicioImagenNodo;
            if (!EsCambio)
            {
                return _Imagen;
            }
            var IzquierdaCentro = 0;
            var DerechaCentro = 0;
            Image IzquierdaNodoImg = null, DerechaNodoImg = null;
            if (HijoIzquierdo != null)
            {
                IzquierdaNodoImg = HijoIzquierdo.Draw(out IzquierdaCentro);
            }
            if (HijoDerecho != null)
            {
                DerechaNodoImg = HijoDerecho.Draw(out DerechaCentro);
            }
            var izquierdaSize = new Size();
            var DerechaSize = new Size();
            var under = (IzquierdaNodoImg != null) || (DerechaNodoImg != null);
            if (IzquierdaNodoImg != null)
            {
                izquierdaSize = IzquierdaNodoImg.Size;
            }
            if (DerechaNodoImg != null)
            {
                DerechaSize = DerechaNodoImg.Size;
            }
            var maxHigh = izquierdaSize.Height;
            if (maxHigh < DerechaSize.Height)
            {
                maxHigh = DerechaSize.Height;
            }
            if (izquierdaSize.Width <= 0)
            {
                izquierdaSize.Width = (_NodoBolsa.Width - _LiberarEspacio.Width) / 2;
            }
            if (DerechaSize.Width <= 0)
            {
                DerechaSize.Width = (_NodoBolsa.Width - _LiberarEspacio.Width) / 2;
            }
            var resize = new Size
            {
                Width = izquierdaSize.Width + DerechaSize.Width + _LiberarEspacio.Width,
                Height = _NodoBolsa.Size.Height + (under ? maxHigh + _LiberarEspacio.Height : 0)
            };
            var ArbolTotal = new Bitmap(resize.Width, resize.Height);
            var g = Graphics.FromImage(ArbolTotal);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), resize));
            var str = Dato.ToString();
            g.DrawImage(_NodoBolsa, izquierdaSize.Width - _NodoBolsa.Width / 2 + _LiberarEspacio.Width / 2 + (2 + (str.Length == 1 ? 10 : str.Length == 2 ? 5 : 0)) * Coef, _NodoBolsa.Height / 2f - 12 * Coef);

            Centro = izquierdaSize.Width + _LiberarEspacio.Width / 2;
            var pen = new Pen(Brushes.White, 1.0f * Coef)
            {
                EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor,
                StartCap = System.Drawing.Drawing2D.LineCap.Round
            };
            float x1 = Centro;
            float y1 = _NodoBolsa.Height;
            float y2 = _NodoBolsa.Height + _LiberarEspacio.Height;
            float x2 = IzquierdaCentro;
            var h = Math.Abs(y2 - y1);
            var w = Math.Abs(x2 - x1);
            if (IzquierdaNodoImg != null)
            {
                g.DrawImage(IzquierdaNodoImg, 0, _NodoBolsa.Size.Height + _LiberarEspacio.Height);
                var points1 = new List<PointF>
                {
                    new PointF(x1, y1),
                    new PointF(x1 - w / 6, y1 + h / 3.0f),
                    new PointF(x2 - w / 6, y1 + h / 3.0f),
                    new PointF(x2, y2),
                };
                g.DrawCurve(pen, points1.ToArray(), 0.1f);
            }
            if (DerechaNodoImg != null)
            {
                g.DrawImage(DerechaNodoImg, izquierdaSize.Width + _LiberarEspacio.Width, _NodoBolsa.Size.Height + _LiberarEspacio.Height);
                x2 = DerechaCentro + izquierdaSize.Width + _LiberarEspacio.Width;
                w = Math.Abs(x2 - x1);
                var points2 = new List<PointF>
                    {
                        new PointF(x1, y1),
                        new PointF(x1 - w / 6, y1 + h / 3.0f),
                        new PointF(x2 - w / 6, y1 + h / 3.0f),
                        new PointF(x2, y2),
                    };
                g.DrawCurve(pen, points2.ToArray(), 0.1f);
            }
            EsCambio = false;
            _Imagen = ArbolTotal;
            _InicioImagenNodo = Centro;
            return ArbolTotal;
        }
    }
}

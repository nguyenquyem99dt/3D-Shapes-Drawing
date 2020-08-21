using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3d_model_LTCK
{
    public class Camera
    {
        // coi góc nhìn camera là 1 góc của 1 khối cầu ngoại tiếp vật ta cần xem
        // góc nhìn
        public double _eyeX, _eyeY, _eyeZ;
        // điểm nhìn(gốc tọa độ mặc định)
        public double _lookX, _lookY, _lookZ;
        public double radius;
        public double theta; // góc của điểm nhìn hợp với trục Oy
        public double phi; // góc của Ox hợp với điểm nhìn 

        public Camera()
        {
            _eyeX = 10;
            _eyeY = 5;
            _eyeZ = 10;
            _lookX = 0;
            _lookY = 0;
            _lookZ = 0;

            ComputeRadius();
            ComputeTheta();
            ComputePhi();
        }

        // Tính góc @theta hiện tại: góc hợp bỏi Ox và Oz
        public void ComputeTheta()
        {
            // Trường hợp điểm nhìn không phải gốc tọa độ thì trừ @_lookX
            theta = Math.Atan((_eyeX - _lookX) / (_eyeZ - _lookZ));
        }

        // Tính góc @phi hiện tại: Góc của Oy với mặt Oxz
        public void ComputePhi()
        {
            // // Trường hợp điểm nhìn không phải gốc tọa độ thì trừ @_lookY
            phi = Math.Asin((_eyeY - _lookY) / radius);
        }

        // Tính bán kính của hình cầu khi thay đổi vị trí camera (khoảng cách từ eye đến look)
        public void ComputeRadius()
        {
            radius = Math.Sqrt(Math.Pow(_eyeX - _lookX, 2)
                     + Math.Pow(_eyeY - _lookY, 2)
                     + Math.Pow(_eyeZ - _lookZ, 2));
        }

        // Phóng to - di chuyển vị trí camera lại gần điểm nhìn
        public void ZoomIn()
        {
            _eyeX += -0.02f * _eyeX;
            _eyeY += -0.02f * _eyeY;
            _eyeZ += -0.02f * _eyeZ;

            // Khi di chuyển vị trí camera thì bán kính hình cầu sẽ thay đổi nên cần cập nhật lại
            ComputeRadius();
            ComputeTheta();
            ComputePhi();
        }

        // Thu nhỏ - di chuyển vị trí camera ra xa điểm nhìn
        public void ZoomOut()
        {
            _eyeX += 0.02f * _eyeX;
            _eyeY += 0.02f * _eyeY;
            _eyeZ += 0.02f * _eyeZ;

            // Khi di chuyển vị trí camera thì bán kính hình cầu sẽ thay đổi nên cần cập nhật lại
            ComputeRadius();
            ComputeTheta();
            ComputePhi();
        }
        // Ta coi góc nhìn camera quay vật thể như 1 khối cầu có bán kính R
        // Di chuyển camera quay xung quanh điểm nhìn sang phải
        // khi xoay phải, tức là ta thay đổi tọa độ góc nhìn eye theo 1 góc theta trên trục Ox và Oz, ở đây trục nằm ngang là Ox và Oz
        // còn trục hướng lên là Oy
        public void RotateRight()
        {
            theta += 0.02f;
            _eyeX = _lookX + radius * Math.Cos(phi) * Math.Sin(theta);
            _eyeZ = _lookZ + radius * Math.Cos(phi) * Math.Cos(theta);
        }

        // Di chuyển camera quay xung quanh điểm nhìn sang trái 
        public void RotateLeft()
        {
            theta -= 0.02f;
            _eyeX = _lookX + radius * Math.Cos(phi) * Math.Sin(theta);
            _eyeZ = _lookZ + radius * Math.Cos(phi) * Math.Cos(theta);
        }

        // Di chuyển camera quay xung quanh điểm nhìn lên trên
        // Khi xoay lên trên tức là ta thay đổi góc nhìn của cả trục Ox, Oy, Oz theo sự tăng dần của góc Phi
        public void RotateUp()
        {
            phi += 0.02f;
            _eyeY = _lookY + radius * Math.Sin(phi);
            _eyeZ = _lookZ + radius * Math.Cos(phi) * Math.Cos(theta);
            _eyeX = _lookX + radius * Math.Cos(phi) * Math.Sin(theta);
        }

        // Di chuyển camera quay xung quanh điểm nhìn xuống dưới
        // Khi xoay xuống dưới tức là ta thay đổi góc nhìn của cả trục Ox, Oy, Oz theo sự giảm dần của góc Phi
        public void RotateDown()
        {
            phi -= 0.02f;

            _eyeY = _lookY + radius * Math.Sin(phi);
            _eyeZ = _lookZ + radius * Math.Cos(phi) * Math.Cos(theta);
            _eyeX = _lookX + radius * Math.Cos(phi) * Math.Sin(theta);

        }
    }
}

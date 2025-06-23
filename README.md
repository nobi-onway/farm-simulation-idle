# 🐮🌾 Farm Simulation Idle Game

Một trò chơi mô phỏng trang trại nhàn rỗi (idle farming simulation) nơi bạn bắt đầu với một nông trại nhỏ và phát triển bằng cách trồng trọt, chăn nuôi và mở rộng. Mục tiêu là đạt được **1.000.000 đồng vàng** bằng cách bán nông sản và sữa bò.

## 🚀 Tính năng chính

- Bắt đầu với:
  - 3 mảnh đất
  - 10 hạt giống cà chua
  - 10 hạt giống việt quất
  - 2 con bò sữa
  - 1 công nhân
  - Thiết bị nông trại cấp 1

- Mỗi loại cây trồng và vật nuôi có:
  - **Thời gian sản xuất**
  - **Sản lượng tối đa (vòng đời)**
  - **Giá bán sản phẩm**

| Loại | Thời gian tạo sản phẩm | Sản phẩm / vòng đời | Giá bán / sản phẩm |
|------|-------------------------|---------------------|---------------------|
| Cà chua | 10 phút / trái | 40 trái | 5 đồng vàng |
| Việt quất | 15 phút / trái | 40 trái | 8 đồng vàng |
| Dâu tây | 5 phút / trái | 20 trái | (Chỉ trồng được sau khi mua) |
| Bò sữa | 30 phút / gallon sữa | 100 gallon | 15 đồng vàng |

- **Thu hoạch đúng hạn**: Có 1 giờ để thu hoạch sau khi sản phẩm cuối cùng được tạo ra. Nếu không, cây/vật nuôi sẽ bị phân huỷ.

- **Công nhân**:
  - Giá thuê: 500 đồng vàng / công nhân
  - Tự động thực hiện công việc trồng, thu hoạch, vắt sữa
  - Mỗi hành động mất 2 phút

- **Nâng cấp thiết bị**:
  - Tăng năng suất cây trồng và vật nuôi thêm 10% mỗi cấp
  - Mỗi lần nâng cấp: 500 đồng vàng

- **Mở rộng đất**:
  - Mở rộng mỗi mảnh đất: 500 đồng vàng
  - Cho phép trồng hoặc nuôi nhiều hơn

- **Cửa hàng**:
  - Hạt giống cà chua: 30 đồng vàng
  - Hạt giống việt quất: 50 đồng vàng
  - Bò sữa giống: 100 đồng vàng
  - Hạt dâu tây: 400 đồng vàng / 10 hạt (chỉ bán buôn)

- **Mục tiêu chiến thắng**: Đạt 1.000.000 đồng vàng

---

## 🛠️ Triển khai dự án

### 1. Clone dự án

```bash
git clone https://github.com/nobi-onway/farm-simulation-idle.git
cd farm-simulation-idle
```

### 2. Mở trong Unity
* Yêu cầu: Unity 2021.3 LTS trở lên (khuyến nghị)
* Mở Unity Hub
* Chọn Add Project, trỏ đến thư mục `farm-simulation-idle`
* Mở project

### 3. Thiết lập & chạy
* Mở scene chính: Assets/Scenes/GameScene.unity
* Nhấn Play để bắt đầu trải nghiệm game

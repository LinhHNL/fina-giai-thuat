import matplotlib.pyplot as plt

# Tạo mảng 2 chiều
dataset = [['Hà Nội', (21.027777, 105.829722)],
 ['Hồ Chí Minh', (10.827778, 106.694444)],
 ['Đà Nẵng', (16.070833, 108.222222)],
 ['Cần Thơ', (10.016667, 105.75)],
 ['Nha Trang', (12.25, 109.166667)],
 ['Huế', (16.45, 107.55)],
 ['Bắc Ninh', (21.02, 106.02)],
 ['Bình Dương', (10.916667, 106.666667)]]

# Tạo bản đồ
plt.figure(figsize=(10, 10))
plt.title("Bản đồ Việt Nam")

# Vẽ các thành phố
for city, coordinates in dataset:
    plt.scatter(coordinates[0], coordinates[1], label=city)

# Vẽ đường đi
x, y = [], []
for i in range(len(dataset) - 1):
    x.append(dataset[i][1][0])
    y.append(dataset[i][1][1])
    plt.plot(x, y, color="red")

# Thêm chú thích
plt.legend()
plt.show()

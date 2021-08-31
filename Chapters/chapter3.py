import cv2

# IMPORT IMAGE
img = cv2.imread("Resources/secretSky.jpg")

# RESIZE IMAGE
imgResize = cv2.resize(img, (300,200))

# CROP IMAGE
imgCropped = img[0:200,200:500]

# SHOW EACH VERSION RENDERED
cv2.imshow("Image", img)
cv2.imshow("Resized Image", imgResize)
cv2.imshow("Cropped Image", imgCropped)

# PRINT SIZES OF IMAGES
print(img.shape)
print(imgResize.shape)
print(imgCropped.shape)

# AWAIT KEYPRESS TO CLOSE WINDOWS
cv2.waitKey(0)

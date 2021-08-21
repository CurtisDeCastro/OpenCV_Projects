# IMPORT DEPENDENCIES
import cv2

### PRINT HELLO WORLD
print("package imported")

### DISPLAY AN IMAGE
img = cv2.imread("Resources/Fire.jpg")
cv2.imshow("Output", img)
cv2.waitKey(1000)

### SETUP AND DISPLAY WEBCAM
cap = cv2.VideoCapture(1) # 1 is address of webcam, as system default (0) was reassigned to GoPro Webcam
cap.set(3,640) # Set width 640px
cap.set(4,480) # Set height 480px
cap.set(10, 100) # Set brightness 100% (the first argument is the setting ID number. Refer to docs for full list)

while True:
    success, img = cap.read()
    cv2.imshow("Video", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


cv2.waitKey(0)


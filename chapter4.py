import cv2
import numpy as np

### CREATE 512 X 512 MATRIX OF BLACK PIXELS
img = np.zeros((512,512,3), np.uint8)
# print(img)

### MAKE PIXELS IN GIVEN RANGE BLUEq
# img[200:300,100:300] = 255,0,0

### DRAW LINE ACROSS IMAGE FROM TOP LEFT TO BOTTOM RIGHT
cv2.line(img,(0,0),(img.shape[1],img.shape[0]),(0,255,0),3)

### DRAW RECTANGLE
cv2.rectangle(img,(0,0),(250,350),(0,0,255),2)

### DRAW CIRCLE
cv2.circle(img,(400,50),30,(255,255,0),5)

### WRITE TEXT
cv2.putText(img," OPENCV TRAP LORDS ",(100,100),cv2.FONT_ITALIC,1,(0,150,0),1)

### SHOW IMAGE
cv2.imshow("Image",img)

### AWAIT KEYPRESS TO EXIT
cv2.waitKey(0)
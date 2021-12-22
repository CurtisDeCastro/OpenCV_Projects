import cv2

##################
frameWidth = 640
frameHeight = 480
cascadePath = "/Users/curtiscastro/Projects/Pycharm/OpenCV_lesson_1/Resources/haarcascades/haarcascade_russian_plate_number.xml"
plateCascade = cv2.CascadeClassifier(cascadePath)
minArea = 500
color = (255, 0, 255)
##################

cap = cv2.VideoCapture(1)
cap.set(3, frameWidth)
cap.set(4, frameHeight)
cap.set(10, 150)

while True:
    success, img = cap.read()
    imgGray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    plates = plateCascade.detectMultiScale(imgGray, 1.1, 10)
    for (x, y, w, h) in plates:
        area = w*h
        print(area)
        if area > minArea:
            cv2.rectangle(img, (x, y), (x + w, y + h), (255, 0, 255), 2)
            cv2.putText(img, "License Plate", (x, y-5), cv2.FONT_HERSHEY_COMPLEX_SMALL, 1, color, 2)
            imgRoi = img[y:y+h, x:x+w]
            cv2.imshow("ROI", imgRoi)
    cv2.imshow("Result", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break
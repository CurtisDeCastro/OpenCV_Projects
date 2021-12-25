import cv2

img1 = cv2.imread("../Resources/A&C-23.jpg")
img2 = cv2.imread("../Resources/A&C-23_cloaked.png")

cascadePath = "/Users/curtiscastro/Projects/Pycharm/OpenCV_lesson_1/Resources/haarcascades/haarcascade_frontalface_default.xml"
faceCascade = cv2.CascadeClassifier(cascadePath)

# First set of keypoints
kpts1 = None

# Find a face!
while kpts1 == None:
    objects = img1.find_features(faceCascade, threshold=0.5, scale=1.25)
    if objects:
        # Expand the ROI by 31 pixels in every direction
        face = (objects[0][0] - 31, objects[0][1] - 31, objects[0][2] + 31 * 2, objects[0][3] + 31 * 2)
        # Extract keypoints using the detect face size as the ROI
        kpts1 = img1.find_keypoints(threshold=10, scale_factor=1.1, max_keypoints=100, roi=face)
        # Draw a rectangle around the first face
        img1.draw_rectangle(objects[0])

print(kpts1)
img1.draw_keypoints(kpts1, size=24)

while (True):
    # Extract keypoints from the whole frame
    kpts2 = img2.find_keypoints(threshold=10, scale_factor=1.1, max_keypoints=100, normalized=True)

    if (kpts2):
        # Match the first set of keypoints with the second one
        c = image.match_descriptor(kpts1, kpts2, threshold=85)
        match = c[6]  # C[6] contains the number of matches.
        if (match > 5):
            img2.draw_rectangle(c[2:6])
            img2.draw_cross(c[0], c[1], size=10)
            print(kpts2, "matched:%d dt:%d" % (match, c[7]))


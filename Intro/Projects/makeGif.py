import cv2
import glob
from PIL import Image

cascadePath = "../Resources/haarcascades/haarcascade_frontalface_default.xml"
faceCascade = cv2.CascadeClassifier(cascadePath)

def make_gif(frame_folder):
    frames = [Image.open(image) for image in glob.glob(f"{frame_folder}/*.jpg")]
    frame_one = frames[0]
    frame_one.save("wedding.gif", format="GIF", append_images=frames,
                   save_all=True, duration=300, loop=0)

while True:
    if __name__ == "__main__":
        print('making gif')
        make_gif("../Resources/BachelorParty")
        print('///DONE///')
        break



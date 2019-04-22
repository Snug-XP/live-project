# -*- coding:utf-8 -*-
from PIL import Image, ImageDraw, ImageFont
import numpy as np

# 图片名称
img = 'post.jpg' # 图片模板
new_img = 'newpost.jpg' # 生成的图片

# 打开图片
image = Image.open(img)
draw = ImageDraw.Draw(image)
width, height = image.size

#设置海报标题
font = ImageFont.truetype("simsun.ttc", 60, encoding="unic")  # 设置字体
draw.text((200, 190), u'中奖名单', 'gold', font)

#从txt中读取文件，用中文"，"分隔结果
file = open("reward.txt","r")
list = file.readlines()
rewards = []
for fields in list:
    fields=fields.strip()
    fields=fields.split("，")
    rewards.append(fields)

#根据得到的结果从数组中获取参数，分别放入奖项与人员中
for index in range(0,len(rewards[0]),2):
    font = ImageFont.truetype("simsun.ttc", 30, encoding="unic")
    draw.text((220,280+20*index), rewards[0][index],'gold',font)
    font = ImageFont.truetype("simsun.ttc", 30, encoding="unic")  # 设置字体
    draw.text((350,280+20*index), rewards[0][index+1], 'gold', font)
# 生成图片
image.save(new_img, 'png')

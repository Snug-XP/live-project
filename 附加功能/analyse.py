# -*- coding: gbk -*-
import jieba
from wordcloud import WordCloud
import re
import xlsxwriter
import imageio
import matplotlib
import matplotlib.pyplot as plt
from xlrd import open_workbook
import numpy as np

#指定默认字体
matplotlib.rcParams['font.sans-serif'] = ['SimHei']
matplotlib.rcParams['font.family']='sans-serif'
#解决负号'-'显示为方块的问题
matplotlib.rcParams['axes.unicode_minus'] = False
fr = open('PlusB.txt', 'r', encoding='utf-8 ')

# 用于之后时间的分段
time_list = []
for i in range(0, 24):
    # 这里的判断用于将类似的‘8’ 转化为 ‘08’ 便于和导出数据匹配
    if i < 10:
        i = '0' + str(i)
    else:
        i = str(i)
    time_list.append(i)

# 用于月份分段
month_list = []
#记录相应月份的聊天数量
month_num = []
for i in range(1, 13):
    if i < 10:
        i = '0' + str(i)
    else:
        i = str(i)
    month_list.append(i)

#  创建EXCEL表格并设置参数
workbook = xlsxwriter.Workbook('hours.xlsx')
worksheet = workbook.add_worksheet()
worksheet.set_column('A:A', 5)
worksheet.set_column('B:B', 10)


# 定义一个函数，对每一个时间段进行计数
# times是正则匹配到的 “小时” 数据，在一个列表里面
def everyhours(i):
    num = 0
    for time in times:
        if time == i:
            num += 1
    # 计数完毕，写入数据，write参数为：行，列，数据
    worksheet.write(int(i), 0, str(i) + "点")
    worksheet.write(int(i), 1, num)

def everymonths(i):
    num=0
    for month in months:
        if month == i:
            num +=1
    month_num.append(num)



# 打开文件，开始匹配“小时”数据，并计数保存
with open("PlusB.txt", encoding='utf-8') as f:
    data = f.read()
    # 时间为20:50:52，要匹配其中的20 月份为2021-12-21
    pa = re.compile(r"(\d\d):\d\d:\d\d")
    pa2 = re.compile(r"\d\d\d\d-(\d\d)-\d\d")
    times = re.findall(pa, data)
    months = re.findall(pa2, data)
    for i in month_list:
        everymonths(i)
    for i in time_list:
        everyhours(i)
    # 记得关闭工作薄
    workbook.close()


x_data=[]
y_data=[]
x_volte=[]
temp=[]
wb = open_workbook('hours.xlsx')
for s in wb.sheets():
    for row in range(s.nrows):
        values = []
        for col in range(s.ncols):
            values.append(s.cell(row,col).value)
        x_data.append(values[0])
        y_data.append(values[1])

ind = np.arange(12) # the x locations for the groups
width = 0.35  # the width of the bars
fig, ax = plt.subplots()
rects1 = ax.bar(ind - width / 2, tuple(month_num), width, color='SkyBlue', label='数量')
ax.set_ylabel('聊天数量')
ax.set_title('活跃度随月份变化图')
plt.xticks(ind, ('一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'))
ax.legend()

fig = plt.figure( figsize=(10, 8), dpi=75, facecolor='#FFFFFF', edgecolor='#0000FF')
plt.plot(x_data, y_data, 'rv-',label=u"发言数",linewidth=2)
plt.title(u"群活跃度变化图")
plt.legend()
plt.xlabel(u"时间段")
plt.ylabel(u"发言数")


frequencies = []
s = ""
data = {}

for line in fr:
    line = line.strip()
    if len(line) == 0:
        continue
    if line[0] == '2':
        continue
    for x in range(0, len(line)):
        if line[x] in [' ', '\t', '\n', '。', '，', '[', ']', '（', '）', ':', '-',
                         '？', '！', '《', '》', '、', '；', '“', '”', '……', '0', '1', '2', '3', '4', '5', '6', '7', '8',
                         '9', '=', '~', '…']:
            continue
        s += str(line[x])

seg_list = jieba.cut(s, cut_all=False, HMM=True)
for word in seg_list:
    if len(word) >= 2:
        if not data.__contains__(word):
            data[word] = 0
        data[word] += 1
    # data=sorted(data.items(),key=lambda d:d[1],reverse=True) 这里必须要注释，不然会报错
    # print(data)

my_wordcloud = WordCloud(
    background_color='white',  # 设置背景颜色
    max_words=150,  # 设置最大实现的字数
    font_path = 'simkai.ttf',  # 设置字体格式，如不设置显示不了中文
    mask=imageio.imread('1.png'),  # 设置图片样式
    width=800,
    height=800,
).generate_from_frequencies(data)
plt.figure()
plt.imshow(my_wordcloud)
plt.axis('off')
plt.show()
my_wordcloud.to_file('词云.jpg')
fr.close()





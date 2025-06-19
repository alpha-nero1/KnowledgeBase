def urlify_string(value):
  retString = ''
  for char in value:
    if (char == ' '):
      retString = retString + "%20"
    else:
      retString = retString + char
  return retString

print("urlify_string \"Ello Govnor\"", urlify_string("Ello Govnor"))
print("urlify_string https//:www.Ello Govnor.com/Apple Pillow", urlify_string("https//:www.Ello Govnor.com/Apple Pillow"))
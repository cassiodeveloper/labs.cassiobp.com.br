import pymongo
import sys

connection = pymongo.MongoClient("mongodb://localhost")

db = connection.teste
collection = db.funnynumbers

magic = 0

try:
    iter = collection.find()
    for item in iter:
        if ((item['value'] % 3) == 0):
            magic = magic + item['value']

except:
    print "Erro ao ler a collection: " + sys.exc_info()[0]

print "A quantidade de itens divisíveis por 3 é " + str(int(magic))
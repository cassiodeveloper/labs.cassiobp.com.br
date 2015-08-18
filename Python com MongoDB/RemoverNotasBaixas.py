import pymongo
import sys

def remover_notas_baixas():
    connection = pymongo.Connection("mongodb://localhost", safe=True)
    db = connection.students
    grades = db.grades

    query = {'type':'homework'}

    try:
        alunos = grades.find(query).sort([('student_id', pymongo.ASCENDING), ('score', pymongo.DESCENDING)])

    except:
        print "Erro: ", sys.exc_info()[0]

    contador = 0

    for aluno in alunos:
        contador += 1
        if contador % 2 == 0:
            print aluno
            grades.remove(aluno)

    return "OK"

print remover_notas_baixas()
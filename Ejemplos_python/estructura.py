from datetime import date, datetime, timedelta
print("Saludo desde el archivo estructura.py")

#Manejo de variables
nombre = "Milena"
edad = 18
estatura = 1.60
print(f"{nombre} tiene {edad} años y mide {estatura} metros")

#conversión de tipos
edad_str = "18"
edad_int = int(edad_str)
print(edad_int + 5)

#Manejo de fechas
fecha_hoy = date.today()
fecha_hora_actual = datetime.now()
cumpleaños = date(2006,12,31)
mañana = date.today() + timedelta(days=1)
dias_transcurridos = (fecha_hoy - date(2025,1,1)).days    
print(dias_transcurridos)

#Manejo de booleans, if y else
es_mayor_de_edad = True
tiene_licencia = False
if es_mayor_de_edad and tiene_licencia:
    print("Puede conducir")
else:
    print("No puede conducir")

#Manejo de rangos if-elif-else
nota = 70
if nota >= 90:
    print("Excelente")
elif nota >= 70:
    print("Aprobado")
else:
    print("Reprobado")

#Simulación de casos
opcion = 2
if opcion == 1:
    print("Opción 1")
elif opcion == 2:
    print("Opción 2")
else:
    print("Opción no válida")

#Simulaciín de switch/case con diccionario
def opcion_1():
    return("Opción 1")
def opcion_2():
    return("Opción 2")
switch = {1: opcion_1, 2: opcion_2}
resultado = switch.get(1, lambda: "Opción no válida")()
print(resultado)

#Bucle (for y while)
#bucle for tradicional
for i in range(1,6):
    print(i)

#bucle while tradicional tradicional
contador = 3
while contador > 0:
    print(contador)
    contador -= 1

#Simulación de bucle do while
while True:
    numero = int(input("Ingresa un número mayor que 0: "))
    if numero > 0:
        break

#Bucle tipo foreach con lista y diccionario
animales = ["gato", "perro", "conejo"]
for animal in animales:
    print(animal)

persona = {"nombre": "Milena", "edad": "18"}
for clave, valor in persona.items(): #items: para ver los indices
    print(f"{clave}:{valor}")



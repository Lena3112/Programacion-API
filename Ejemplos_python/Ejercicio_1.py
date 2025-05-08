#Crear un programa que pida al usuario su nombre y edad, y le diga si puede votar(mayor que 18)

nombre = str(input("Ingrese su nombre: "))
edad= int(input("Ingrese su edad: "))
if edad >= 18:
    print(f"{nombre}, puede votar")
else:
    print(f"{nombre}, no puede votar")
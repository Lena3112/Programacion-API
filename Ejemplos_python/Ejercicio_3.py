#Crear un programa ue almacene una lista de frutas y permita al usuario buscar si una fruta está en la lista

separador = ", " 
fruta_disponible =["pera", "manzana", "uvas", "fresas"]
cadena_unida = separador.join(fruta_disponible) 
buscar = str(input("Ingrese la fruta que desa buscar: "))

if buscar in fruta_disponible:
    print(f"Se encontró la fruta: {buscar}")
else:
    print(f"No se encontró la fruta: {buscar} las frutas disponibles son: {cadena_unida}")
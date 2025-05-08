#Simula un cajero automático que permita al usuario retirar dinero hasta que no tenga saldo

saldo = 1000 
print(f"Tu saldo inicial es: {saldo}$")

while saldo > 0:
    try:
        retiro = float(input("¿Cuánto deseas retirar?"))

        if retiro <= 0:
            print("Ingresa una cantidad mayor a cero.")
        elif retiro > saldo:
            print(f"No tienes suficiente saldo. Tu saldo actual es: {saldo}$")
        else:
            saldo -= retiro
            print(f"Has retirado ${retiro}. Saldo restante: {saldo}$")

    except ValueError:
        print("Entrada no válida. Por favor ingresa un número.")

print("Saldo agotado")

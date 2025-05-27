from pydantic import BaseModel
from fastapi import FastAPI, HTTPException
import mysql.connector
from mysql.connector import Error

# app = FastAPI()

# personas_db = []

# @app.get("/")
# def read_root():
#     return {"mensaje": "Hola mundo con FastAPI"}

# @app.get("/saludo/{nombre}")
# def saludar(nombre: str):
#     return {"mensaje": f"Hola {nombre}, bienvenido a FastAPI"}

# class Persona(BaseModel):
#     nombre: str
#     edad: int
#     estatura: float


# @app.post("/crear-persona")
# def crear_persona(persona: Persona):
#     # Guardar la persona en la lista
#     personas_db.append(persona)
#     return {
#         "mensaje": f"{persona.nombre} registrada con {persona.edad} años y {persona.estatura} metros de estatura."
#     }

# @app.get("/consultar-personas")
# def consultar_personas():
#     # Devolver todas las personas registradas
#     return {"personas": personas_db}

# @app.delete("/eliminar-persona/{nombre}")
# def eliminar_persona(nombre: str):
#     global personas_db
#     personas_db = [p for p in personas_db if p.nombre != nombre]
#     return {"mensaje": f"Persona {nombre} eliminada"}


# @app.put("/actualizar-persona/{nombre}")
# def actualizar_persona(nombre: str, datos_actualizados: Persona):
#     for i, persona in enumerate(personas_db):
#         if persona.nombre == nombre:
#             personas_db[i] = datos_actualizados
#             return {"mensaje": f"Persona {nombre} actualizada exitosamente"}
#     return {"error": f"No se encontró la persona con nombre {nombre}"}


# Configuración de conexión
db_config = {
    'host': 'localhost',
    'user': 'root',
    'password': '',
    'database': 'fastapi_db'
}

app = FastAPI()

# Modelo Pydantic
class Persona(BaseModel):
    nombre: str
    edad: int
    estatura: float

# Función para conectar a la base de datos
def get_connection():
    try:
        conn = mysql.connector.connect(**db_config)
        return conn
    except Error as e:
        raise HTTPException(status_code=500, detail=str(e))

# Crear tabla si no existe
def crear_tabla():
    conn = get_connection()
    cursor = conn.cursor()
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS personas (
            nombre VARCHAR(100) PRIMARY KEY,
            edad INT,
            estatura FLOAT
        )
    """)
    conn.commit()
    conn.close()

crear_tabla()

@app.get("/")
def read_root():
    return {"mensaje": "Hola mundo con FastAPI"}

@app.get("/saludo/{nombre}")
def saludar(nombre: str):
    return {"mensaje": f"Hola {nombre}, bienvenido a FastAPI"}

@app.post("/crear-persona")
def crear_persona(persona: Persona):
    conn = get_connection()
    cursor = conn.cursor()
    try:
        cursor.execute(
            "INSERT INTO personas (nombre, edad, estatura) VALUES (%s, %s, %s)",
            (persona.nombre, persona.edad, persona.estatura)
        )
        conn.commit()
        return {"mensaje": f"{persona.nombre} registrada correctamente."}
    except mysql.connector.IntegrityError:
        raise HTTPException(status_code=400, detail="La persona ya está registrada.")
    finally:
        conn.close()

@app.get("/consultar-personas")
def consultar_personas():
    conn = get_connection()
    cursor = conn.cursor(dictionary=True)
    cursor.execute("SELECT * FROM personas")
    personas = cursor.fetchall()
    conn.close()
    return {"personas": personas}

@app.delete("/eliminar-persona/{nombre}")
def eliminar_persona(nombre: str):
    conn = get_connection()
    cursor = conn.cursor()
    cursor.execute("DELETE FROM personas WHERE nombre = %s", (nombre,))
    conn.commit()
    conn.close()
    return {"mensaje": f"Persona {nombre} eliminada"}

@app.put("/actualizar-persona/{nombre}")
def actualizar_persona(nombre: str, datos_actualizados: Persona):
    conn = get_connection()
    cursor = conn.cursor()
    cursor.execute(
        "UPDATE personas SET nombre = %s, edad = %s, estatura = %s WHERE nombre = %s",
        (datos_actualizados.nombre, datos_actualizados.edad, datos_actualizados.estatura, nombre)
    )
    if cursor.rowcount == 0:
        conn.close()
        raise HTTPException(status_code=404, detail=f"No se encontró la persona con nombre {nombre}")
    conn.commit()
    conn.close()
    return {"mensaje": f"Persona {nombre} actualizada exitosamente"}
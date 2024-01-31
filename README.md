# Sistema de Parqueos

Este proyecto es un sistema de parqueos desarrollado en .NET para gestionar el estacionamiento de vehículos para residentes, no residentes y vehículos oficiales.

## Configuración

Antes de ejecutar la aplicación, es necesario configurar las cadenas de conexión a la base de datos. Esto se puede hacer en el archivo `appsettings.json`. Asegúrate de modificar las siguientes secciones con la información de la base de datos:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Aquí va tu cadena de conexión"
  }
}
```

## Migración de Base de Datos

Para inicializar la base de datos, utiliza la consola de administración de paquetes de NuGet. Abre la consola y ejecuta el siguiente comando:

```bash
update-database
```
Este comando aplicará todas las migraciones pendientes y creará las tablas necesarias en la base de datos.

## Requisitos del Sistema

Asegúrate de tener instalado lo siguiente en tu entorno de desarrollo:

* .NET8 SDK
* Visual Studio 2022
* SQL Server 2016 minimo

## Ejecución del Proyecto

1. Abre el proyecto en Visual Studio.
2. Configura las cadenas de conexión en `appsettings.json`.
3. Ejecuta el comando `update-database` desde la consola de administración de paquetes para migrar la base de datos.
4. Compila y ejecuta la aplicación.


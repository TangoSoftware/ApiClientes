<a name="inicio"></a>
# Tango Software - ApiClientes

Este repositorio incluye el código fuente y la documentación necesaria para la puesta en marcha de la API de obtención de JSONs de comprobantes de TANGO Gestión para los clientes deseados.

 + [Puesta en marcha](#instalacion)
    + [Versiones soportadas de Tango Gestión](#versiones)
    + [Precondiciones](#precondiciones)
    + [Ambientes](#ambientes)
    + [Configurar API](#configApi)
 + [Modo de uso](#modouso)
    + [Utilización de la API](#usoApi)
    + [Utilización de la URL de notificacion](#usoNotif)
    + [Datos del Json](#djson)
 + [Preguntas frecuentes](#pregfrec)



<a name="instalacion"></a>
## Puesta en marcha
[<sub>Volver</sub>](#inicio)

<a name="versiones"></a>
### Versiones soportadas de Tango Gestión
[<sub>Volver</sub>](#inicio)

La versión mínima requerida de Tango Gestión para consumir los comprobantes en formato JSON es:
XX.YY.ZZZZ o superior.

<a name="precondiciones"></a>
### Precondiciones de funcionamiento
[<sub>Volver</sub>](#inicio)

Para poner a disposición los  JSONs de los comprobantes se requiere configurar previamente en Tango Gestión las siguientes funciones:

• Dentro del ABM de Clientes marcar aquellos clientes que desea sincronizar con la aplicación Nexo Clientes para que publiquen comprobantes. 

• Parametrizar el certificado digital fiscal en Tango para obtener el CAE de sus comprobantes electrónicos.(La API sólo entrega comprobantes electrónicos con CAE asignado por AFIP).

• La empresa de Tango, cuyos comprobantes en JSON desea entregar vía API, vinculada a través de Tango Sync a la aplicación Nexo Clientes.

<a name="ambientes"></a>
### Ambientes
[<sub>Volver</sub>](#inicio)

• Ambiente de testeo

Para configurar el ambiente de testeo desde Tango Sync debe vincular una empresa de nube con una empresa ejemplo de Tango Gestión.

• Ambiente de producción

Para configurar el ambiente de producción desde Tango Sync debe vincular una empresa de nube con una empresa operativa de Tango Gestión.


<a name="configApi"></a>
### Configurar API
[<sub>Volver</sub>](#inicio)

Luego de haber vinculado una empresa de nube con una empresa de Tango Gestión, acceda a nexo Clientes / API para parametrizar el servicio que entrega los JSONs de los comprobantes electrónicos.

(Pulse en el opción "API" para acceder a su configuración).

![imagen api](https://github.com/TangoSoftware/ApiClientes/blob/master/men%C3%BA.JPG)




En esta pantalla observará:

• **Token:** Es el token de acceso al servicio que provee los JSON.

• **Correo electrónico de los interesados:** Es el correo electrónico de a quiénes se desea informar acerca de la utilización de la API.

• **Notificar nuevos comprobantes a la URL:** Es la URL a la cual se desea informar cuando se tengan novedades de nuevos comprobantes disponibles para consumir en formato JSON.

Pulse el botón &quot;Aceptar&quot; para confirmar los cambios

<a name="modouso"></a>
## Modo de uso
[<sub>Volver</sub>](#inicio)

<a name="usoApi"></a>
### Utilización de la API
[<sub>Volver</sub>](#inicio)

Al recibir el correo electrónico obtendrá el detalle de como utilizar la API de comprobantes JSON.
A continuación se detalla de manera técnica como está compuesta la API, qué métodos expone y como se parametrizan.

#### Método: getjsonfrom

- Tipo: GET

- Objetivo: Entregar los JSON de comprobantes cuya fecha de emisión sea mayor a una fecha dada.

- Url del servicio: https://tclientes.axoft.com/api/comprobantes/getjsonfrom/{id_de_cliente} 

- Composición del header:

   - Token: Es el obtenido por mail. El mismo que se visualiza al ingresar a la vista API.
    
   - From : Es la fecha y hora a partir de la cual se desean obtener los comprobantes JSON. Es una fecha y hora en formato UTC con formato yyyy-MM-dd o yyyy-MM-ddTHH:mm:ss (si se deja vacío se utilizará la fecha del día en que se solicitó al servicio).
    

#### Método: getnotdownloadedjsonfrom

- Tipo: GET

- Objetivo: Entregar JSON de comprobantes que aún no fueron entregados por el servicio a partir de una fecha dada. 

- Url del servicio: https://tclientes.axoft.com/api/comprobantes/getnotdownloadedjsonfrom/{id_cliente} 

- Composición del header:

   - Token: Es el obtenido por mail. El mismo que se visualiza al ingresar a la vista API.
    
   - From : Es la fecha y hora a partir de la cual se desean obtener los comprobantes JSON. Es una fecha y hora en formato UTC con formato yyyy-MM-dd o yyyy-MM-ddTHH:mm:ss (si se deja vacío se utilizará la fecha del día en que se solicitó al servicio).
   
#### Método: getjson

- Tipo: GET

- Objetivo: Entregar JSON específico para un id de cliente y id de comprobante en particular.  

- Url del servicio: https://tclientes.axoft.com/api/comprobantes/getjson/{id_cliente}/{id_comprobante}.

   - id_cliente: Es el id de cliente asociado al servicio. Viene especificado en el mail. De todos modos se puede obtener     consultando la vista de configurar api.
   
   - id_comprobante: id del comprobante específico del cual se desea obtener el JSON. **(Es requerido tener configurada la URL de notificación para que este método sea de utilidad, ya que es la notificación la que entrega el id de comprobante nuevo, disponible para ser consumido. Ver [URL de notificación](#notificaciones) para más información).** 
   
- Composición del header:

   - Token: Es el obtenido por mail. El mismo que se visualiza al ingresar a la vista API.
    
   - From : Es la fecha y hora a partir de la cual se desean obtener los comprobantes JSON. Es una fecha y hora en formato UTC con formato yyyy-MM-dd o yyyy-MM-ddTHH:mm:ss (si se deja vacío se utilizará la fecha del día en que se solicitó al servicio).



<a name="usoNotif"></a>
### Utilización de la URL de notificación
[<sub>Volver</sub>](#inicio)

De modo adicional, se puede parametrizar en la configuración de la API en Nexo Clientes, una URL a la cual enviar una novedad cada vez que un cliente ponga a disposición un nuevo JSON asociado a un comprobante.

Dicha URL deberá cumplir con el estándar XXXX, siendo de la forma:
www.ejemplo.com
o
https://ejemplo.com/

En este repositorio se deja a disponibilidad


<a name="djson"></a>
### Detalle y composición del JSON
[<sub>Volver</sub>](#inicio)

VER EL README DEL REPO DE NEXO TIENDAS PARA MAYOR ORIENTACION


<a name="pregfrec"></a>
## Preguntas frecuentes


- **¿PREG FREC 1?**

| Respuesta1. |
| --- |




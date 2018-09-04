<a name="inicio"></a>
# Tango Software - ApiClientes

Este repositorio incluye el código fuente y la documentación necesaria para la puesta en marcha de la API de obtención de JSONs de comprobantes de Tango Gestión para los clientes deseados.

 + [Puesta en marcha](#instalacion)
    + [Versiones soportadas de Tango Gestión](#versiones)
    + [Precondiciones](#precondiciones)
    + [Ambientes](#ambientes)
    + [Configurar API](#configApi)
 + [Modo de uso](#modouso)
    + [Utilización de la API](#usoApi)
    + [Utilización de la URL de notificacion](#usoNotif)
    + [Consola de ejemplo](#Console)
    + [Datos del Json](#djson)


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

Para poner a disposición los JSONs de los comprobantes se requiere configurar previamente en Tango Gestión las siguientes funciones:

* Dentro del ABM de Clientes marcar aquellos clientes que desea sincronizar con la aplicación Nexo Clientes para que publiquen comprobantes. 

* Parametrizar el certificado digital fiscal en Tango para obtener el CAE de sus comprobantes electrónicos.(La API sólo entrega comprobantes electrónicos con CAE asignado por AFIP).

* La empresa de Tango, cuyos comprobantes en JSON desea entregar vía API, vinculada a través de Tango Sync a la aplicación Nexo Clientes.

<a name="ambientes"></a>
### Ambientes
[<sub>Volver</sub>](#inicio)

#### Ambiente de testeo

Para configurar el ambiente de testeo desde Tango Sync debe vincular una empresa de nube con una empresa ejemplo de Tango Gestión.

#### Ambiente de producción

Para configurar el ambiente de producción desde Tango Sync debe vincular una empresa de nube con una empresa operativa de Tango Gestión.

<a name="configApi"></a>
### Configurar API
[<sub>Volver</sub>](#inicio)

Luego de haber vinculado una empresa de nube con una empresa de Tango Gestión, acceda a nexo Clientes/API para parametrizar el servicio que entrega los JSONs de los comprobantes electrónicos.

(Pulse en el opción "API" para acceder a su configuración).

![imagen api](https://github.com/TangoSoftware/ApiClientes/blob/master/men%C3%BA.JPG)

En esta pantalla observará:

* **Token:** es el token de acceso al servicio que provee los JSON.

* **Correo electrónico de los interesados:** es el correo electrónico de aquellos a quienes se desea informar acerca de la utilización de la API.

* **Notificar nuevos comprobantes a la URL:** es la URL a la cual se desea informar cuando se tengan novedades de nuevos comprobantes disponibles para consumir en formato JSON.

Pulse el botón &quot;Aceptar&quot; para confirmar los cambios.

<a name="modouso"></a>
## Modo de uso
[<sub>Volver</sub>](#inicio)

<a name="usoApi"></a>
### Utilización de la API
[<sub>Volver</sub>](#inicio)

Al recibir el correo electrónico obtendrá el detalle de como utilizar la API de JSONs de comprobantes. A continuación se detalla como está compuesta la API, qué métodos expone y como se parametrizan.

#### Método: getjsonfrom

* Tipo: GET.

* Objetivo: entregar los JSON de comprobantes cuya fecha de emisión sea mayor a una fecha dada.

* URL del servicio: https://tclientes.axoft.com/api/comprobantes/getjsonfrom/{id_de_cliente} 

* Composición del header:

   * Token: es el obtenido por correo electrónico, y se visualiza al ingresar a la vista de configuración de la API.
    
   * From: es la fecha y hora a partir de la cual se desean obtener los JSONs de los comprobantes. Es una fecha y hora en formato UTC con formato yyyy-MM-dd o yyyy-MM-ddTHH:mm:ss (si se deja vacío se utilizará la fecha del día en que se solicitó al servicio).
    

#### Método: getnotdownloadedjsonfrom

* Tipo: GET.

* Objetivo: entregar JSON de comprobantes que aún no fueron entregados por el servicio a partir de una fecha dada. 

* Url del servicio: https://tclientes.axoft.com/api/comprobantes/getnotdownloadedjsonfrom/{id_cliente} 

* Composición del header:

   * Token: es el obtenido por correo electrónico, y se visualiza al ingresar a la vista de configuración de la API.
    
   * From: es la fecha y hora a partir de la cual se desean obtener los JSONs de los comprobantes. Es una fecha y hora en formato UTC con formato yyyy-MM-dd o yyyy-MM-ddTHH:mm:ss (si se deja vacío se utilizará la fecha del día en que se solicitó al servicio).
   
#### Método: getjson

* Tipo: GET.

* Objetivo: entregar JSON específico para un ID de cliente y un ID de comprobante en particular.  

* URL del servicio: https://tclientes.axoft.com/api/comprobantes/getjson/{id_cliente}/{id_comprobante}.

   * id_cliente: es el ID de cliente asociado al servicio. Viene especificado en el correo electrónico. Además, puede ser obtenido consultando la vista de configuración de la API.
   
   * id_comprobante: es el ID del comprobante específico del cual se desea obtener el JSON. **(Es obligatorio tener configurada la URL de notificación para que este método sea de utilidad, ya que es la notificación la que entrega el ID de comprobante nuevo, disponible para ser consumido. Ver [URL de notificación](#notificaciones) para más información).** 
   
* Composición del header:

   * Token: es el obtenido por correo electrónico, y se visualiza al ingresar a la vista de configuración de la API.
    
   * From: es la fecha y hora a partir de la cual se desean obtener los JSONs de los comprobantes. Es una fecha y hora en formato UTC con formato yyyy-MM-dd o yyyy-MM-ddTHH:mm:ss (si se deja vacío se utilizará la fecha del día en que se solicitó al servicio).

<a name="usoNotif"></a>
### Utilización de la URL de notificación
[<sub>Volver</sub>](#inicio)

Se puede parametrizar en la configuración de la API en Nexo Clientes, una URL a la cual enviar una novedad cada vez que un cliente ponga a disposición un nuevo JSON asociado a un comprobante. Para funcionar, se debe completar el campo **Notificar nuevos comprobantes a la URL**. Dicha URL deberá cumplir con el estándar [RFC 1738](https://www.rfc-es.org/rfc/rfc1738-es.txt), ya que la configuración rechazará cualquier UL con formato inválido.

Ante novedades de nuevos comprobantes publicados en Nexo Clientes, se enviará un request a la URL parametrizada con el ID de cliente y el ID de comprobante a notificar. Cumplirá el siguiente formato: http://www.ejemplo.com/Id_cliente/Id_comprobante

<a name="example"></a>
### Aplicación web de ejemplo
[<sub>Volver</sub>](#inicio)

Este repositorio incluye el código fuente de una aplicación web de ejemplo, desarrollada en ASP.NET con .NET Framework 4.6.1. Dicha aplicación puede ser utilizada para recibir las notificaciones de nuevos comprobantes, y permite leer los JSONs de dichos comprobantes. Para ello, deberá:

 1. Clonar este repositorio.
 
 2. Modificar en el archivo web.config los valores de las claves "IdCliente" y "Token" por los valores correspondientes que figuran en el correo electrónico recibido al configurar la API de Tango Clientes. Estos valores se corresponden con el cliente de cuyos nuevos comprobantes desea ser notificado.
 
 3. Desplegar la aplicación en un servidor propio.
 
 4. Incluir en la configuración de la API de Tango Clientes la URL correspondiente a esta aplicación desplegada en su servidor. Es decir, si la URL de la web en su servidor es http://www.miservidordeejemplo.com/, entonces la URL a notificar será http://www.miservidordeejemplo.com/home/notificar

En la vista principal de la aplicación web figuran el ID del cliente, el token de seguridad, y la lista de nuevos comprobantes. Pdrá ver el JSON de cada comprobante o, directamente, marcarlo como leído, lo cual lo quitará inmediatamente de dicha lista.
![imagen api](https://github.com/TangoSoftware/ApiClientes/blob/master/list.png)

En la vista del JSON del comprobante correspondiente, podrá ver el JSON, copiarlo y marcarlo como leído, lo cual también lo quitará inmediatamente de la lista de nuevos comprobantes.
![imagen api](https://github.com/TangoSoftware/ApiClientes/blob/master/json.png)

<a name="djson"></a>
### Detalle y composición del JSON
[<sub>Volver</sub>](#inicio)

VER EL README DEL REPO DE NEXO TIENDAS PARA MAYOR ORIENTACION

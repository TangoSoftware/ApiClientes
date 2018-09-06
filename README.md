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
18.01.000 o superior.

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

Se puede parametrizar en la configuración de la API en Nexo Clientes, una URL a la cual enviar una novedad cada vez que un cliente ponga a disposición un nuevo JSON asociado a un comprobante. Para funcionar, se debe completar el campo **Notificar nuevos comprobantes a la URL**. Dicha URL deberá cumplir con el estándar [RFC 1738](https://www.rfc-es.org/rfc/rfc1738-es.txt), ya que la configuración rechazará cualquier URL con formato inválido.

Ante novedades de nuevos comprobantes publicados en Nexo Clientes, se enviará un request a la URL parametrizada con el ID de cliente y el ID de comprobante a notificar. Cumplirá el siguiente formato: http://www.ejemplo.com/Id_cliente/Id_comprobante

<a name="example"></a>
### Aplicación web de ejemplo
[<sub>Volver</sub>](#inicio)

Este repositorio incluye el código fuente de una aplicación web de ejemplo, desarrollada en ASP.NET con .NET Framework 4.6.1. Dicha aplicación puede ser utilizada para recibir las notificaciones de nuevos comprobantes, y permite leer los JSONs de dichos comprobantes. Para ello, deberá:

 1. Clonar este repositorio.
 
 2. Modificar en el archivo web.config los valores de las claves "IdCliente" y "Token" por los valores correspondientes que figuran en el correo electrónico recibido al configurar la API de Tango Clientes. Estos valores se corresponden con el cliente de cuyos nuevos comprobantes desea ser notificado.
 
 3. Desplegar la aplicación en un servidor propio.
 
 4. Incluir en la configuración de la API de Tango Clientes la URL correspondiente a esta aplicación desplegada en su servidor. Es decir, si la URL de la web en su servidor es http://www.miservidordeejemplo.com/, entonces la URL a notificar será http://www.miservidordeejemplo.com/home/notificar. La URL que contiene está aplicación es a modo de ejemplo y coincide con {URL del servidor}/home/notificar.



En la vista principal de la aplicación web figuran el ID del cliente, el token de seguridad, y la lista de nuevos comprobantes. Pdrá ver el JSON de cada comprobante o, directamente, marcarlo como leído, lo cual lo quitará inmediatamente de dicha lista.

![imagen lista](https://github.com/TangoSoftware/ApiClientes/blob/master/list.png)


En la vista del JSON del comprobante correspondiente, podrá ver el JSON, copiarlo y marcarlo como leído, lo cual también lo quitará inmediatamente de la lista de nuevos comprobantes.

![imagen json](https://github.com/TangoSoftware/ApiClientes/blob/master/json.png)

<a name="djson"></a>
### Detalle y composición del JSON
[<sub>Volver</sub>](#inicio)

### Formato del JSON

{  
 "InformacionComprobante":{  
  &quot;TipoDeComprobante&quot;:&quot;FAC&quot;,  
  &quot;NumeroDeComprobante&quot;:&quot;A000100000881&quot;,  
  &quot;PuntoDeVenta&quot;:&quot;0001&quot;,  
  &quot;CodigoTalonario&quot;:&quot;1&quot;,  
  &quot;FechaDeEmision&quot;:&quot;2018-09-04T00:00:00&quot;,  
  &quot;AutorizacionComprobanteElectronico&quot;:{  
   &quot;Codigo&quot;:68184648262638,  
   &quot;FechaDeVencimiento&quot;:&quot;2018-10-03T00:00:00.000&quot;,  
   &quot;ValidacionAfip&quot;:&quot;A&quot;  
  }  
},  

 "InformacionComprobanteReferencia":{  
  &quot;TipoDeComprobanteReferencia&quot;:&quot;REM&quot;,  
  &quot;NumeroDeComprobanteReferencia&quot;:&quot;X000100000070&quot;  
},  

 "InformacionVendedor":{  
  &quot;Cuit&quot;:&quot;30-22111111-3&quot;,  
  &quot;CondicionIVA&quot;:&quot;RI&quot;  
},  

 "InformacionComprador":{  
  &quot;TipoDocumento&quot;:&quot;CUIT&quot;,  
  &quot;NumeroDocumento&quot;:&quot;50-00000391-0&quot;,  
  &quot;Denominacion&quot;:&quot;Distribuidora Lombardini&quot;,  
  &quot;CondicionIngresosBrutos&quot;:&quot;M Multilateral&quot;,  
  &quot;NumeroIngresosBrutos&quot;:&quot;03034560303456892422&quot;,  
  &quot;Domicilio&quot;:&quot;Cerrito 1186&quot;,  
  &quot;Localidad&quot;:&quot;Capital Federal&quot;,  
  &quot;Provincia&quot;:&quot;08 CHUBUT&quot;,  
  &quot;CodigoPostal&quot;:&quot;1010&quot;,  
  &quot;Email&quot;:&quot;DistribuidoraLombardini@gmail.com&quot;  
},  

 "Items":[  
  {  
   &quot;Codigo&quot;:&quot;030006528&quot;,  
   &quot;Descripcion&quot;:&quot;AURICULARES MDR-110LP&quot;,  
   &quot;ImporteTotal&quot;:121.00,  
   &quot;Cantidad&quot;:1.0000000000,  
   &quot;Unidad&quot;:&quot;UNI&quot;,  
   &quot;PrecioUnitario&quot;:100.0,  
   &quot;ImporteDescuento&quot;:0.00,  
   &quot;Impuestos&quot;:[  
   {  
   &quot;CodigoImpuesto&quot;:&quot;1&quot;,  
   &quot;CodigoAFIP&quot;:&quot;5&quot;,  
   &quot;PorcentajeAlicuota&quot;:21.00,  
   &quot;Importe&quot;:21.00  
   }  
   ]  
  }  
],  

 "Totales":{  
  &quot;ImporteTotalNetoGravado&quot;:100.00,  
  &quot;ImporteOperacionesExentas&quot;:0.00,  
  &quot;ImporteTotalFactura&quot;:121.00,  
  &quot;TipoDeCambio&quot;:1.0,  
  &quot;CodigoMoneda&quot;:&quot;PES&quot;,  
  &quot;DescuentoRecargo&quot;:{  
   &quot;ImporteDescuento&quot;:0.00,  
   &quot;PorcentajeDescuento&quot;:0.00,  
   &quot;PorcentajeRecargo&quot;:0.00,  
   &quot;PorcentajeInteres&quot;:0.00  
  },  
  &quot;Impuestos&quot;:[  
   {  
   &quot;CodigoImpuesto&quot;:&quot;1&quot;,  
   &quot;PorcentajeImpuesto&quot;:21.00,  
   &quot;ImporteImpuesto&quot;:21.00,  
   &quot;ImporteGravado&quot;:100.00,  
   &quot;CodigoAFIP&quot;:&quot;5&quot;  
   }  
  ]  
},  

 "Extensiones":{  
  &quot;ExtensionMediosDePago&quot;:{  
   &quot;Pagos&quot;:[  
   {  
   &quot;CondicionVenta&quot;:3,  
   &quot;Codigo&quot;:&quot;CC&quot;,  
   &quot;Descripcion&quot;:&quot;30/60/90 CON INTERES&quot;,  
   &quot;Importe&quot;:121.000  
   }  
   ],    
   &quot;Vencimientos&quot;:[  
   {  
   &quot;FechaVencimiento&quot;:&quot;2018-11-03T00:00:00-03:00&quot;,  
   &quot;ImporteCuota&quot;:121.00  
   }  
   ]  
  }  
}  
}

<br/><br/>

### Datos del JSON
  

**Tópico InformacionComprobante**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **TipoDeComprobante** | Si | Tipo de comprobante | Varchar(3) | FAC |
| **NumeroDeComprobante** | Si | Número de comprobante | Varchar(13) | A000100000881 |
| **PuntoDeVenta** | Si | Número de punto de venta | Varchar(4) | 0001 |
| **CodigoTalonario** | Si | Número de talonario | ENTERO_TG(2) | 1 |
| **FechaDeEmision** | Si | Fecha de emisión del comprobante | Datetime | 2018-09-04 00:00:00 |
| **AutorizacionComprobanteElectronico (Código)** | No | CAE informado por AFIP | Varchar(14) | 68184648262638 |
| **AutorizacionComprobanteElectronico (FechaDeVencimiento)** | No | Fecha de vencimiento del CAE | Datetime | 2018-10-03 00:00:00.000 |
| **AutorizacionComprobanteElectronico (ValidacionAfip)** | No | Estado de la autorización | Varchar(1) | 'A' = Aprobado / 'P'= Pendiente / 'R' = Rechazado |

<br/><br/>

**Tópico InformacionComprobanteReferencia**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **TipoDeComprobanteReferencia** | No | Tipo de comprobante de referencia | Varchar(3) | REM |
| **NumeroDeComprobanteReferencia** | No | Número de comprobante de referencia | Varchar(13) | X000100000070 |

<br/><br/>

**Tópico InformacionVendedor**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **Cuit** | Si | C.U.I.T. | Varchar(13) | 30-22111111-3 |
| **CondicionIVA** | Si | Condición frente al I.V.A. | Varchar(3) | RI |

<br/><br/>

**Tópico InformacionComprador**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **TipoDocumento** | No | Tipo de documento | smallint(2) | CUIT |
| **NumeroDocumento** | No | Número de documento | Varchar(20) | 50-00000391-0 |
| **Denominacion** | Si | Nombre del cliente | Varchar(60) | Distribuidora Lombardini |
| **CondicionIngresosBrutos** | Si |  Condición de Ingresos Brutos | Varchar(1) | ' ' =   No liquida / 'L' = Local / 'M'= Multilateral / 'S' = Simplificado |
| **NumeroIngresosBrutos** | No | Número de Ingresos Brutos | Varchar(20) | 03034560303456892422 |
| **Domicilio** | Si | Domicilio del cliente | Varchar(30) | Cerrito 1186 |
| **Localidad** | Si | Localidad del cliente | Varchar(20) | Capital Federal |
| **Provincia** | Si | Provincia del cliente (Código y Descripción) | Varchar(2) y Varchar(20) | 08 CHUBUT |
| **CodigoPostal** | Si | Código postal del cliente | Varchar(8) | 1010 |
| **Email** | No | Email del cliente | Varchar(255) | DistribuidoraLombardini@gmail.com |

<br/><br/>

**Tópico Items**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **Codigo** | Si | Código de artículo | Varchar(15) | 030006528 |
| **Descripcion** | No | Descripción del artículo | Varchar(30) | AURICULARES MDR-110LP |
| **ImporteTotal** | Si | Importe total del renglón | DECIMAL_TG(13) | 121.00 |
| **Cantidad** | Si | Cantidad del artículo | DECIMAL_TG(13) | 1.0000000000 |
| **Unidad** | Si | Unidad de medida del artículo | Varchar(10) | UNI |
| **PrecioUnitario** | Si | Precio unitario del artículo | DECIMAL_TG(13) | 100.0000000 |
| **ImporteDescuento** | Si | Importe del descuento del renglón | DECIMAL_TG(13) | 0.00 |
| **Impuestos (CodigoImpuesto)** | Si | Código del impuesto | Varchar(2) | 1 |
| **Impuestos (CodigoAFIP)** | Si | Código A.F.I.P. | Varchar(3) | 5 |
| **Impuestos (PorcentajeAlicuota)** | Si | Porcentaje del impuesto | DECIMAL_TG(13) | 21.00 |
| **Impuestos (Importe)** | Si | Importe del impuesto | DECIMAL_TG(13) | 21.00 |

<br/><br/>

**Tópico Totales**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **ImporteTotalNetoGravado** | Si | Importe del neto gravado | DECIMAL_TG(13) | 100.00 |
| **ImporteOperacionesExentas** | Si | Importe de operaciones exentas | DECIMAL_TG(13) | 0.00 |
| **ImporteTotalFactura** | Si | Importe de la factura | DECIMAL_TG(13) | 121.00 |
| **TipoDeCambio** | Si | Cotización | DECIMAL_TG(13) | 1.0 |
| **CodigoMoneda** | No | Código de moneda | Char(3) | PES |
| **DescuentoRecargo (ImporteDescuento)** | Si | Importe del descuento | DECIMAL_TG(13) | 0.00 |
| **DescuentoRecargo (PorcentajeDescuento)** | Si | Porcentaje del descuento | DECIMAL_TG(13) | 0.00 |
| **DescuentoRecargo (PorcentajeRecargo)** | Si | Porcentaje del recargo | DECIMAL_TG(13) | 0.00 |
| **DescuentoRecargo (PorcentajeInteres)** | Si | Porcentaje de interés | DECIMAL_TG(13) | 0.00 |
| **Impuestos (CodigoImpuesto)** | Si | Código del impuesto | ENTERO_TG(2) | 1 |
| **Impuestos (PorcentajeImpuesto)** | Si | Porcentaje del impuesto | DECIMAL_TG(13) | 21.00 |
| **Impuestos (ImporteImpuesto)** | Si | Importe del impuesto | DECIMAL_TG(13) | 21.00 |
| **Impuestos (ImporteGravado)** | Si | Importe gravado | DECIMAL_TG(13) | 100.00 |
| **Impuestos (CodigoAFIP)** | Si | Código A.F.I.P. | Varchar(3) | 5 |

<br/><br/>

**Tópico Extensiones**

| **Campo** | **Requerido** | **Descripción** | **Tipo de Dato** | **Valores Posibles / Ejemplos** |
| --- | --- | --- | --- | --- |
| **ExtensionMediosDePago (Pagos {CondicionVenta})** | Si | Condición de venta | DECIMAL_TG(13) | 3 |
| **ExtensionMediosDePago (Pagos {Codigo})** | Si | Codigo de cuenta | DECIMAL_TG(13) | 'CC'= Cuenta Corriente o el código de la cuenta ‘Efectivo’ |
| **ExtensionMediosDePago (Pagos {Descripcion})** | Si | Descripción | DECIMAL_TG(13) | 30/60/90 CON INTERES |
| **ExtensionMediosDePago (Pagos {Importe})** | Si | Importe total del pago | DECIMAL_TG(13) | 121.000 |
| **ExtensionMediosDePago (Vencimientos {FechaVencimiento})** | No | Fecha de vencimiento de la cuota | Datetime | 2018-10-03 00:00:00-03:00 |
| **ExtensionMediosDePago (Vencimientos {ImporteCuota})** | No | Importe de la cuota | DECIMAL_TG(13) | 121.00 |

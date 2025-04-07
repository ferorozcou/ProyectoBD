# ProyectoBD
*Integrantes:* Fernanda Orozco, Victoria Madriz, Maria Rasero, Lara Durán
  
**Descripción del proyecto:**

Consiste en un juego de simulación donde el jugador gestiona un restaurante, inspirado en la serie de *Papa’s Games*. El juego se centra en la preparación de pedidos siguiendo las instrucciones de los clientes. 

Los personajes son una variedad de animalitos, donde el jugador toma el papel de un perrito. Sirve a un gato, un conejo, un pato y una capibara, cada uno con una personalidad y exigencias distintas, y tiene como jefe a un lobo, quien le explica las instrucciones al jugador. 

Al iniciar el juego, el jugador tiene la  posibilidad de escoger entre tres restaurantes distintos: uno con temática venezolana que sirve arepas, uno de temática mexicana cuyo plato estrella son los sopes y, finalmente, un restaurante español especializado en tortillas. Dentro de estos restaurantes, el jugador dispone de diez ingredientes distintos para preparar los pedidos de los clientes, separados entre bases, relleno y extras.

Cada restaurante cuenta con varios niveles que van aumentando de dificultad progresivamente. Cada ingrediente correcto por pedido suma cierta cantidad de puntos y, si al terminarse el tiempo límite el jugador ha logrado alcanzar el mínimo de puntos establecido, podrá avanzar al siguiente nivel. A medida que aumentas de nivel, desbloqueas ingredientes, productos y negocios.  

**Modelo Entidad-Relación del proyecto:**

![ModeloEntidadRelacion](https://github.com/user-attachments/assets/7525a12a-b58b-4dff-8005-76c730b6c465)

**Descripción del diagrama:**

Dentro del diagrama tenemos las siguientes entidades:
- Jugador (
Atributos: Id, Nombre, Apariencia.
Relacionado con: Nivel, Restaurante)
- Nivel (
Atributos: Id, Nº pedidos, Tiempo, Puntos requeridos.
Relacionado con: Jugador, Restaurante)
- Restaurante (
Atributos: Nombre, Tipo, Recompensa, Apariencia.
Relacionado con:Jugador, Ingrediente)
- Ingrediente (
Atributos: Id, Tipo, Nombre, Imagen, Restaurante.
Relacionado con: Pedido)
- Pedido (
Atributos: Id.
Relacionado con:Ingrediente, Bebida, Tipo Pedido)
- Bebida (
Atributos: Nombre, Apariencia.
Relacionado con: Pedido, Cliente)
- Cliente (
Atributos: Animal, Frase fácil, Frase difícil, Apariencia.
Relacionado con: Bebida)
- TipoPedido (
Atributos: Dificultad, Nº elementos, P. Perfecto, P. 1 error, P. 2 errores, P. 3+ errores.
Relacionado con: Pedido)

Encontramos las relaciones:
- Jugador-Nivel (1 a 1): Cada jugador puede jugar un único nivel a la vez y cada nivel solo incluye un jugador.
- Jugador-Restaurante (1 a 1):  Cada jugador puede jugar un único restaurante al mismo tiempo y cada restaurante solo puede contener un jugador.
- Restaurante-Ingrediente (1 a n):  Cada restaurante tiene varios ingredientes, pero cada ingrediente corresponde a un único restaurante.
- Ingrediente-Pedido (n a m): Cada pedido contiene varios ingredientes y cada ingrediente puede encontrarse en varios pedidos.
- Pedido-Bebida (1 a n): Cada bebida puede ser encontrada en varios pedidos, pero cada pedido incluye una única bebida.
- Bebida-Cliente (1 a 1): Cada cliente pide una bebida específica y cada bebida puede ser pedida por un solo cliente.
- Pedido-TipoPedido (1 a n): Cada pedido puede ser de un único tipo pero cada tipo puede encontrarse en más de un pedido.
- Nivel-TipoPedido (n a m): Cada nivel incluye varios tipos de pedido y cada tipo de pedido puede encontrarse en más de un nivel.



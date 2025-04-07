# ProyectoBD
*Integrantes:* Fernanda Orozco, Victoria Madriz, Maria Rasero, Lara Durán
  
**Descripción del proyecto:**

Consiste en un juego de simulación donde el jugador gestiona un restaurante, inspirado en la serie de *Papa’s Games*. El juego se centra en la preparación de pedidos siguiendo las instrucciones de los clientes. 

Los personajes son una variedad de animalitos, donde el jugador toma el papel de un perrito. Sirve a un gato, un conejo, un pato y una capibara, cada uno con una personalidad y exigencias distintas, y tiene como jefe a un lobo, quien le explica las instrucciones al jugador. 

Al iniciar el juego, el jugador tiene la  posibilidad de escoger entre tres restaurantes distintos: uno con temática venezolana que sirve arepas, uno de temática mexicana cuyo plato estrella son los sopes y, finalmente, un restaurante español especializado en tortillas. Dentro de estos restaurantes, el jugador dispone de diez ingredientes distintos para preparar los pedidos de los clientes, separados entre bases, relleno y extras.

Cada restaurante cuenta con varios niveles que van aumentando de dificultad progresivamente. Cada ingrediente correcto por pedido suma cierta cantidad de puntos y, si al terminarse el tiempo límite el jugador ha logrado alcanzar el mínimo de puntos establecido, podrá avanzar al siguiente nivel. A medida que aumentas de nivel, desbloqueas ingredientes, productos y negocios.  

**Modelo Entidad-Relación del proyecto:**

![ModeloEntidadRelacion](https://github.com/user-attachments/assets/7525a12a-b58b-4dff-8005-76c730b6c465)

Descripción del diagrama:
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
-Tipo Pedido (
Atributos: Dificultad, Nº elementos, P. Perfecto, P. 1 error, P. 2 errores, P. 3+ errores.
Relacionado con: Pedido)

El jugador trabaja en un restaurante y juega en diferentes niveles, los cuales incluyen distintos restaurantes. Cada restaurante tiene ingredientes específicos. Los pedidos están compuestos por ingredientes y bebidas. Los clientes ordenan bebidas específicas y están caracterizados por frases que varían según la dificultad del nivel y apariencia. Los pedidos pertenecen a un tipo de pedido que define su dificultad y criterios de evaluación (perfecto, con errores, etc.).


INSERT INTO Clientes (Animal, FraseFacil, FraseDificil) VALUES
    ('Pato', 'Y una co-quack-cola por favor', 'Quack, Quack, ya lo sabes'),
    ('Capibara', 'Y un agua por favor', 'Ya te la sabes'),
    ('Gato', 'Y un Sprite, miau, miau', 'Miau, la de siempre'),
    ('Conejo', 'Y me pones un zumo, tío', 'Ponme lo de siempre'),
    ('Koala', 'Y un Ice… Tea', 'Zzzz');

INSERT INTO Bebidas (Nombre, Cliente) VALUES
    ('Agua', 'Capibara'),
    ('Cocacola', 'Pato'),
    ('Sprite', 'Gato'),
    ('Zumo', 'Conejo'),
    ('Ice Tea', 'Koala');

INSERT INTO Restaurantes (Tipo, Recompensa, Nombre) VALUES
('Español', 'Clavel', 'Tios Españoleria'),
('Venezolano', 'Cuatro', 'Maricos Venezoleria'),
('Mexicano', 'Sombrero', 'Weys mexicaneria');

INSERT INTO Ingredientes (Id, Nombre, Tipo, Restaurante) VALUES
    (1, 'Tortilla', 'Base', 'Español'),
    (2, 'Cebolla', 'Relleno', 'Español'),
    (3, 'Chorizo', 'Relleno', 'Español'),
    (4, 'Espinaca', 'Relleno', 'Español'),
    (5, 'Bacalao', 'Relleno', 'Español'),
    (6, 'Jamón', 'Relleno', 'Español'),
    (7, 'Alioli', 'Extra', 'Español'),
    (8, 'Mayonesa', 'Extra', 'Español'),
    (9, 'Ketchup', 'Extra', 'Español'),
    (10, 'Arepas', 'Base', 'Venezolano'),
    (11, 'Reina Pepiada', 'Relleno', 'Venezolano'),
    (12, 'Queso de mano', 'Relleno', 'Venezolano'),
    (13, 'Pelúa', 'Relleno', 'Venezolano'),
    (14, 'Dominó', 'Relleno', 'Venezolano'),
    (15, 'Catira', 'Relleno', 'Venezolano'),
    (16, 'Perico', 'Relleno', 'Venezolano'),
    (17, 'Salsa picante', 'Toppings', 'Venezolano'),
    (18, 'Salsa de ajo', 'Toppings', 'Venezolano'),
    (19, 'Guasacaca', 'Toppings', 'Venezolano'),
    (20, 'Sopes', 'Base', 'Mexicano'),
    (21, 'Frijoles blancos', 'Relleno', 'Mexicano'),
    (22, 'Frijoles negros', 'Relleno', 'Mexicano'),
    (23, 'Pollo', 'Toppings', 'Mexicano'),
    (24, 'Chorizo', 'Toppings', 'Mexicano'),
    (25, 'Crema', 'Toppings', 'Mexicano'),
    (26, 'Queso', 'Toppings', 'Mexicano'),
    (27, 'Lechuga', 'Toppings', 'Mexicano'),
    (28, 'Aguacate', 'Toppings', 'Mexicano');

INSERT INTO Niveles (Id, NumeroPedidos, PuntosRequeridos, TiempoSegundos) VALUES
(1, 5, 500, 120),
(2, 7, 700, 120),
(3, 10, 1000, 130);

INSERT INTO TiposPedidos (Dificultad, NumeroElementos, PuntosPerfecto, Puntos1Error, Puntos2Errores, Puntos3Errores) VALUES
('Fácil', 1, 120, 100, 50, 0),
('Medio', 3, 120, 100, 70, 20),
('Difícil', 5, 130, 100, 80, 50);

INSERT INTO TiposPedidosNiveles (IdNivel, TipoPedido, Cantidad) VALUES
(1, 'Fácil', 3),
(1, 'Medio', 2),
(2, 'Fácil', 2),
(2, 'Medio', 4),
(2, 'Difícil', 1),
(3, 'Fácil', 2),
(3, 'Medio', 5),
(3, 'Difícil', 3);
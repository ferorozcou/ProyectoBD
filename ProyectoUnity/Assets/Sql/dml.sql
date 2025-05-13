INSERT INTO Bebidas (Nombre, Apariencia, Cliente) VALUES
    ('Agua', 'Pato', NULL),
    ('Cocacola', 'Capibara', NULL),
    ('Sprite', 'Gato', NULL),
    ('Zumo', 'Conejo', NULL),
    ('Ice Tea', 'Koala', NULL);

INSERT INTO Clientes (Animal, FraseFacil, FraseDificil, Apariencia) VALUES
    ('Pato', 'Una co-quack-cola por favor', 'Quack, Quack, ya lo sabes', NULL),
    ('Capibara', 'Un agua por favor', 'Ya te la sabes', NULL),
    ('Gato', 'Un Sprite, miau, miau', 'Miau, la de siempre', NULL),
    ('Conejo', 'Me pones un zumo, tío', 'Ponme lo de siempre', NULL),
    ('Koala', 'Ice… Tea', 'Zzzz', NULL);

INSERT INTO Restaurantes (Tipo, Recompensa, Apariencia, Nombre) VALUES
('Español', 'Clavel', NULL, 'Tio''s Españoleria'),
('Venezolano', 'Cuatro', NULL, 'Marico''s Venezoleria'),
('Mexicano', 'Sombrero', NULL, 'Wey''s mexicaneria');

INSERT INTO Ingredientes (Id, Nombre, Tipo, Restaurante, Imagen) VALUES
    (1, 'Tortilla', 'Base', 'Español', NULL),
    (2, 'Cebolla', 'Relleno', 'Español', NULL),
    (3, 'Chorizo', 'Relleno', 'Español', NULL),
    (4, 'Espinaca', 'Relleno', 'Español', NULL),
    (5, 'Bacalao', 'Relleno', 'Español', NULL),
    (6, 'Jamón', 'Relleno', 'Español', NULL),
    (7, 'Alioli', 'Extra', 'Español', NULL),
    (8, 'Mayonesa', 'Extra', 'Español', NULL),
    (9, 'Ketchup', 'Extra', 'Español', NULL),
    (10, 'Arepas', 'Base', 'Venezolano', NULL),
    (11, 'Reina Pepiada', 'Relleno', 'Venezolano', NULL),
    (12, 'Queso de mano', 'Relleno', 'Venezolano', NULL),
    (13, 'Pelúa', 'Relleno', 'Venezolano', NULL),
    (14, 'Dominó', 'Relleno', 'Venezolano', NULL),
    (15, 'Catira', 'Relleno', 'Venezolano', NULL),
    (16, 'Perico', 'Relleno', 'Venezolano', NULL),
    (17, 'Salsa picante', 'Toppings', 'Venezolano', NULL),
    (18, 'Salsa de ajo', 'Toppings', 'Venezolano', NULL),
    (19, 'Guasacaca', 'Toppings', 'Venezolano', NULL),
    (20, 'Sopes', 'Base', 'Mexicano', NULL),
    (21, 'Frijoles blancos', 'Relleno', 'Mexicano', NULL),
    (22, 'Frijoles negros', 'Relleno', 'Mexicano', NULL),
    (23, 'Pollo', 'Toppings', 'Mexicano', NULL),
    (24, 'Chorizo', 'Toppings', 'Mexicano', NULL),
    (25, 'Crema', 'Toppings', 'Mexicano', NULL),
    (26, 'Queso', 'Toppings', 'Mexicano', NULL),
    (27, 'Lechuga', 'Toppings', 'Mexicano', NULL),
    (28, 'Aguacate', 'Toppings', 'Mexicano', NULL);

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
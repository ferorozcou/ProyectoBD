CREATE TABLE IF NOT EXISTS "Bebidas" (
	"Nombre"	TEXT NOT NULL UNIQUE,
	"Apariencia"	TEXT,
	"Cliente"	TEXT,
	PRIMARY KEY("Nombre"),
	FOREIGN KEY("Cliente") REFERENCES "Clientes"("Animal")
);

CREATE TABLE IF NOT EXISTS "Clientes" (
	"Animal"	TEXT NOT NULL UNIQUE,
	"FraseFacil"	TEXT,
	"FraseDificil"	TEXT,
	"Apariencia"	TEXT,
	PRIMARY KEY("Animal")
);

CREATE TABLE IF NOT EXISTS "Ingredientes" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Nombre"	TEXT,
	"Tipo"	TEXT,
	"Restaurante"	TEXT,
	"Imagen"	TEXT,
	PRIMARY KEY("Id"),
	FOREIGN KEY("Restaurante") REFERENCES "Restaurantes"
);

CREATE TABLE IF NOT EXISTS "Niveles" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"NumeroPedidos"	INTEGER,
	"PuntosRequeridos"	INTEGER,
	"TiempoSegundos"	INTEGER,
	PRIMARY KEY("Id")
);

CREATE TABLE IF NOT EXISTS "Pedidos" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Tipo"	TEXT,
	"Bebida"	TEXT,
	PRIMARY KEY("Id"),
	FOREIGN KEY("Bebida") REFERENCES "Bebidas"("Nombre"),
	FOREIGN KEY("Tipo") REFERENCES "TiposPedidos"("Dificultad")
);

CREATE TABLE IF NOT EXISTS "PedidosIngredientes" (
	"IdPedido"	INTEGER NOT NULL,
	"IdIngrediente"	INTEGER NOT NULL,
	PRIMARY KEY("IdPedido","IdIngrediente"),
	FOREIGN KEY("IdIngrediente") REFERENCES "Ingredientes",
	FOREIGN KEY("IdPedido") REFERENCES "Pedidos"
);

CREATE TABLE IF NOT EXISTS "Restaurantes" (
	"Tipo"	TEXT NOT NULL UNIQUE,
	"Recompensa"	TEXT,
	"Apariencia"	TEXT,
	"Nombre"	TEXT,
	PRIMARY KEY("Tipo")
);

CREATE TABLE IF NOT EXISTS "TiposPedidos" (
	"Dificultad"	TEXT NOT NULL UNIQUE,
	"NumeroElementos"	INTEGER,
	"PuntosPerfecto"	INTEGER,
	"Puntos1Error"	INTEGER,
	"Puntos2Errores"	INTEGER,
	"Puntos3Errores"	INTEGER,
	PRIMARY KEY("Dificultad")
);

CREATE TABLE IF NOT EXISTS "TiposPedidosNiveles" (
	"IdNivel"	INTEGER NOT NULL,
	"TipoPedido"	TEXT NOT NULL,
	"Cantidad"	INTEGER,
	PRIMARY KEY("IdNivel","TipoPedido"),
	FOREIGN KEY("IdNivel") REFERENCES "Niveles",
	FOREIGN KEY("TipoPedido") REFERENCES "TiposPedidos"
);
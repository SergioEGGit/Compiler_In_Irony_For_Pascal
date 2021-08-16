// Regla No. 1

// Eliminar Codigo
goto L1;

    T1 = T2;
	T1 = T2;
	T1 = T2;
	T1 = T2;
	T1 = T2;
	T1 = T2;
	printf("%d", (int) 59);
	Metodo();
	Stack[(int) T1] = 10;
	
L1:

	T3 = T1 + T3;
	
// No Eliminar Codigo Existe Una Etiqueta En Medio	
goto L2;

    T1 = T2;
	T1 = T2;
	T1 = T2;
	printf("%d", (int) 59);

L1:
	
	T1 = T2;
	T1 = T2;
	T1 = T2;
	printf("%d", (int) 59);
	
L2:

	T3 = T1 + T3;
	printf("%d", (int) 59);
	
	
// Regla No. 2

if(T1 == 4) goto L1;
goto L2;
L1:

	T1 = T2;
	T1 = T2;
	T1 = T2;
	T1 = T2;
	T1 = T2;
	T1 = T2;
	printf("%d", (int) 59);
	
L2:

	T1 = T2;
	T1 = T2;
	T1 = T2;
	Metodo();
	T1 = T2;
	T1 = T2;
	T1 = T2;
	printf("%d", (int) 59);
	
	
// Regla No.3

// Aplicar Regla 
if (1 == 1) goto L1; 
goto L2;
L1:

   T1 = T2 + 10;
   printf("%d", (int) 59);
   
L2:

	T2 = T3 + 10;
	Metodo();
	
// No Aplicar Regla 
if (1 != 1) goto L1; 
goto L2;
L1:

   T1 = T2 + 10;
   
L2:

	T2 = T3 + 10;	
	
// Regla No.4
if (4 == 1) goto L1; 
goto L2;	
L1:

	T1 = T2 + 10;
	T1 = T2 + 10;
	T1 = T2 + 10;
	printf("%d", (int) 59);
	Metodo();
	
L2:

	T1 = T2 + 10;
	T1 = T2 + 10;
	T1 = T2 + 10;
	Stack[(int) 10] = 10;
	
// No Aplicar Regla	
if (4 != T1) goto L1; 
goto L2;	
L1:

	T1 = T2 + 10;
	T1 = T2 + 10;
	T1 = T2 + 10;
	printf("%d", (int) 59);
	Metodo();
	
L2:

	T1 = T2 + 10;
	T1 = T2 + 10;
	T1 = T2 + 10;
	Stack[(int) 10] = 10;	
	
// Regla No.5

// Aplicar Regla 
T3 = T2;

Metodo();

T1 = T2 + T3;

T2 = T3;

// No Aplicar Regla Existe Etiqueta 
T30 = T21;

L1 :
	Metodo();

	T1 = T2 + T3;

	T21 = T30;

// No Aplicar Regla Existe Cambio De Valor 
T50 = T60;

T50 = 10;

Metodo();

T1 = T2 + T3;

T60 = T50;

// Regla No.6 
T1 = T1 + 0;
T1 = 0 + T1;

// Regla No.7
T1 = T1 - 0;

// Regla No.8
T1 = T1 * 1;
T1 = 1 * T1;

// Regla No.9
T1 = T1 / 1;

// Regla No.10
T1 = T2 + 0;
T1 = 0 + T2;

// Regla No.11
T1 = T2 - 0;

// Regla No.12
T1 = T2 * 1;
T1 = 1 * T2;

// Regla No.13 
T1 = T2 / 1;

// Regla No.14 
T1 = T2 * 2;
T1 = 2 * T2;

// Regla No.15
T1 = T2 * 0;
T1 = 0 * T2;

// Regla No.16
T1 = 0 / T2;
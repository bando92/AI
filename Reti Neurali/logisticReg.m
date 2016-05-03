%% Regressione logistica
clear ; close all; clc
%debug_on_error(1);

data = load('MyTest8.txt');
y = data(:, 1);
LWD = data(:, [2, 3]);
RWD = data(:, [4, 5]);
X = multiply(LWD(:,1),LWD(:,2),RWD(:,1),RWD(:,2));
%% ==================== Plot dati ====================
fprintf(['Plot dati.\n + esempi LWD(y = 1) e o esempi RWD (y = 0).\n']);
plotData(X, y);

% Legende nel grafico 
hold on;
xlabel('Left Weight')
ylabel('Right Weight')
legend('Left', 'Right','Balance')
hold off;

%% ============ Calcolo Costo e Gradiente ============
xx = 0;
yy = sigmoid(xx);
printf("Calcolata sigmoide, per x=%f vale y=%f\n",xx,yy);

printf("\nSigmoidi varie, per controllo:\n");
xx = [-1 0 1;-10 0 10];
yy = sigmoid(xx);
disp(yy);

% Calcolo dimensioni dell'esempio
X1 = X(1:413,:);
X2 = X(414:625,:);
[m, n] = size(X1);
[m1, n1] = size(X2);
%[m, n] = size(X);

% Aggiunge gli uni
X1 = [ones(m, 1) X1];
X2 = [ones(m1, 1) X2];
%X = [ones(m, 1) X];

% Inizializzazione del theta
initial_theta = zeros(n + 1, 1);

% Calcolo costo e gradienti iniziali
[cost, grad] = costFunction(initial_theta, X1, y(1:413));
%[cost, grad] = costFunction(initial_theta, X, y);
fprintf('\nCosto con theta nullo: %f\n', cost);
fprintf('Gradienti con theta nullo: %f \n', grad);

%% ============= Ottimizzazione con GD
alpha = 0.0005;
num_iters = 500;
new_theta = gradientDescent(X1,y(1:413),initial_theta, alpha, num_iters);
%new_theta = gradientDescent(X,y,initial_theta, alpha, num_iters);
fprintf('\nTheta da GD:\n');
disp(new_theta);

% Calcola l'accuratezza sul training set 
[p, count, count2] = classifica(new_theta, X2, y(414:625));
%[p, count, count2] = classifica(new_theta, X, y);
fprintf('Accuratezza sul test set con GD: %f\n\n', mean(double(p == y(414:625))) * 100);
%fprintf('Accuratezza sul traininig set con GD: %f\n\n', mean(double(p == y)) * 100);
fprintf('Count: %d  Count2: %d\n\n', count, count2);
%% ============= Ottimizzazione con fminunc  =============

%  Opzioni di fminunc
options = optimset('GradObj', 'on', 'MaxIter', 400);

%  fminunc calcola i theta ottimi, ritorna theta e costo
%[theta, cost] = ...
%   fminunc(@(t)(costFunction(t, X, y)), initial_theta, options);
[theta, cost] = ...
   fminunc(@(t)(costFunction(t, X1, y(1:413))), initial_theta, options);

% Stampa risultati
fprintf('Costo col theta calcolato da fminunc: %f\n', cost);
fprintf('theta: %f \n', theta);

% Plot Boundary
plotDecisionBoundary(theta, X2, y(414:625));
%plotDecisionBoundary(theta, X, y);

% Legende 
hold on;
xlabel('Left Weight')
ylabel('Right Weight')
legend('Left', 'Right','Balance')
hold off;

%% ============== Predizione e accuratezza ==============
prob = sigmoid([1 20 5] * theta);
fprintf(['In una bilancia con LWD di 20 e RWD di 5, si prevede una ' ...
         'probabilita di classificarla pendente a sinistra di %f\n\n'], prob);

fprintf('\nFINE.\n');
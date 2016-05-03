% COSTFUNCTION costo e gradiente per regressione logistica
function [J, grad] = costFunction(theta, X, y)

m = length(y); % num esempi

% inizializzazione
J = 0;
grad = zeros(size(theta));

% calcolo
J = sum(-y .* log(sigmoid(X * theta)) - (1-y).* log(1-sigmoid(X * theta)))/m;
grad = (X' * (sigmoid(X * theta) - y))/m;

end
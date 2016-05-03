% PLOTDECISIONBOUNDARY Plot dei dati e del corrispondente decision boundary 
function plotDecisionBoundary(theta, X, y)
% Dati: + esempi positivi e o esempi negativi. 
% X pu� essere
% 1) matrice m x 3, prima colonna tutti 1 (caso 2d)
% 2) matrice m x n, n>3 matrix, prima colonna tutti 1

% Plot
plotData(X(:,2:3), y);
hold on

if size(X, 2) <= 3
    % Bidimensinale, boundary � una retta. Calolo x e y degli estremi
    plot_x = [min(X(:,2))-2,  max(X(:,2))+2];
    plot_y = (-1./theta(3)).*(theta(2).*plot_x + theta(1));

    % Plot
    plot(plot_x, plot_y)
    
    % Legenda
    legend('Approvato', 'Non approvato', 'Decision Boundary')
    axis([0, 26, 0, 26])
else
    % La griglia di calcolo
    u = linspace(-1, 1.5, 50);
    v = linspace(-1, 1.5, 50);

    % Calcola z = theta*x sulla griglia
    z = zeros(length(u), length(v));
    for i = 1:length(u)
        for j = 1:length(v)
            z(i,j) = mapFeature(u(i), v(j))*theta;
        end
    end
    z = z'; % trasposizione per il contour

    % Plot z = 0
    % Necessario il range [0, 0]
    contour(u, v, z, [0, 0], 'LineWidth', 2)
end
hold off

end
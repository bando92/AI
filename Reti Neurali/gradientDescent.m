% GRADIENTDESCENT gradient descent sui parametri theta
function [theta, J_history] = gradientDescent(X, y, theta, alpha, num_iters)

[cost, dummy] = costFunction(theta, X, y);
printf('\nGradient descent:\niter %d cost %f\n', 0,cost);

m = length(y);
J_history = zeros(num_iters, 1);

for iter = 1:num_iters
   theta -= (alpha/m).*(X'*(sigmoid(X * theta)-y));
   [J_history(iter), dummy] = costFunction(theta, X, y);
   if (mod(iter,50) == 0)
      printf('iter %d cost %f\n', iter,J_history(iter));
   endif
end

end
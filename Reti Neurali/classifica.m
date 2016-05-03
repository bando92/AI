function [p, count, count2] = classifica(theta, X, y)

m = size(X, 1);

p = zeros(m, 1);
count = 0;
count2 = 0;
p = sigmoid(X*theta);

for i=(1:m)
  if(p(i)>= 0.5)
    p(i) = 1;
    if(y(i) == 1)
      count2 = count2 + 1;
    endif
    count = count+1;
  else
    p(i) = 0;
  endif
endfor

end
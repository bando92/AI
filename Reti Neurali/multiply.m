function X = multiply(LW,LD,RW,RD)
  X = zeros(size(LW),1);
  for k = 1:size(LW)
    X(k, 1) = LW(k)*LD(k);
    X(k, 2) = RW(k)*RD(k);
  end
end
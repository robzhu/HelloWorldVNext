FROM mono:3.10

RUN apt-get -qq update && apt-get -qqy install unzip

RUN curl -s https://raw.githubusercontent.com/aspnet/Home/dev/kvminstall.sh | sh
RUN bash -c "source /root/.kre/kvm/kvm.sh \
      && kvm upgrade \
      && kvm alias default | xargs -i ln -s /root/.kre/packages/{} /root/.kre/packages/default"

ENV PATH $PATH:/root/.kre/packages/default/bin
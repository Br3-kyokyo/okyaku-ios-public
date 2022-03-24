using System.Collections.Generic;

class active_record {

    active_record () {
        //動的メソッド生成 - 外部キー
    }

    void has_one (string class_name) {

    }

    void has_many (string class_name) {

    }

    Dictionary<string, dynamic> association_mapping;

    public void include (string class_name) {

    }

    public dynamic withdraw (string class_name) {
        dynamic hoge;
        association_mapping.TryGetValue (class_name, out hoge);
        return hoge;
    }
}
